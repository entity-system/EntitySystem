using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntitySystem.Server.Exceptions;
using EntitySystem.Server.Extensions;
using EntitySystem.Server.Tree;
using EntitySystem.Shared.Domain;
using EntitySystem.Shared.Exceptions;
using EntitySystem.Shared.Models;
using EntitySystem.Shared.Query;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace EntitySystem.Server.Services;

public class QueryService
{
    private readonly QueryStoreService _queryStoreService;
    private readonly DatabaseService _databaseService;

    public QueryService(QueryStoreService queryStoreService, DatabaseService databaseService)
    {
        _queryStoreService = queryStoreService;
        _databaseService = databaseService;
    }

    public static string GetUniqueName<TEntity>(string prefix = null, Guid? guid = null)
    {
        return $"{prefix}{typeof(TEntity).Name}{(guid ?? Guid.NewGuid()).ToSqlAlias()}Unique{Guid.NewGuid().ToSqlAlias()}";
    }

    public static ICriterion AggregateRestrictions<TEntity>(IEnumerable<Expression<Func<TEntity, bool>>> restrictions, ICriterion seed = null)
    {
        return restrictions.Aggregate(seed, (c, n) => c == null ? Restrictions.Where(n) : Restrictions.And(c, Restrictions.Where(n)));
    }

    public Guid Query<TEntity>(IList<Expression<Func<TEntity, bool>>> restrictions = null)
        where TEntity : class, IEntity
    {
        var master = new MasterNode();

        AddCreator(master, restrictions);

        AddJoiner(master, restrictions);

        return master.Guid = _queryStoreService.Nodes.Add(master);
    }

    public static void AddCreator<TEntity>(MasterNode node, IEnumerable<Expression<Func<TEntity, bool>>> restrictions = null)
        where TEntity : class, IEntity
    {
        (ICriteria criteria, string field) Creator(ISession session)
        {
            var field = GetUniqueName<TEntity>();

            var criteria = session.CreateCriteria<TEntity>(field);

            var constraint = AggregateRestrictions(restrictions ?? Enumerable.Empty<Expression<Func<TEntity, bool>>>());

            if (constraint != null) criteria.Add(constraint);

            return (criteria, field);
        }

        node.Creator = Creator;
    }

    public Guid Join<TEntity>(Guid parentGuid, QueryJoin queryJoin, IEnumerable<Expression<Func<TEntity, bool>>> restrictions = null)
    {
        var parent = _queryStoreService.Nodes.Peek(parentGuid);

        var child = new JoinNode { QueryJoin = queryJoin };

        AddJoiner(child, restrictions);

        child.Assign(parent);

        return child.Guid = _queryStoreService.Nodes.Add(child);
    }

    public static void AddJoiner<TEntity>(TableNode node, IEnumerable<Expression<Func<TEntity, bool>>> restrictions = null)
    {
        (ICriteria criteria, string childField) Applier(Guid guid, ICriteria criteria, string parentKey, string childProperty)
        {
            var childField = GetUniqueName<TEntity>(nameof(Join), guid);

            var childKey = $"{childField}.{childProperty}";

            var constraint = AggregateRestrictions(restrictions ?? Enumerable.Empty<Expression<Func<TEntity, bool>>>(), Restrictions.EqProperty(parentKey, childKey));

            criteria = criteria.CreateEntityAlias(childField, constraint, JoinType.LeftOuterJoin, typeof(TEntity).FullName);

            return (criteria, childField);
        }

        node.Joiner = Applier;
    }

    public async Task<PageList<TEntity>> List<TEntity>(Guid guid, Query query)
        where TEntity : class, IEntity
    {
        var session = _databaseService.GetSession();

        var queryNode = _queryStoreService.Nodes.Pop(guid) ?? throw new GeneralFriendlyException("Unable to get query node.");

        var masterNode = queryNode.GetRoot() as MasterNode ?? throw new GeneralFriendlyException("Unable to get master node.");

        var masterListCriteria = QueryMaster(session, query, masterNode);

        var masterCountCriteria = QueryMaster(session, query, masterNode, true);

        var masterListCriteriaSql = masterListCriteria.ToSqlString();

        var masterCountCriteriaSql = masterCountCriteria.ToSqlString();

        var masterCount = masterCountCriteria.FutureValue<int>();

        var masterList = await masterListCriteria.ListAsync<long>();

        var entityBatches = await QueryBatchesAsync().ToListAsync();

        async IAsyncEnumerable<IEnumerable<TEntity>> QueryBatchesAsync()
        {
            foreach (var id in masterList) yield return await QueryBatch<TEntity>(queryNode, session, id).GetEnumerableAsync();
        }

        return new PageList<TEntity>{Page = entityBatches.SelectMany(l => l).ToList(), MasterCount = masterCount.Value};
    }

    public IFutureEnumerable<TEntity> QueryBatch<TEntity>(INode node, ISession session, long joinId)
        where TEntity : class, IEntity
    {
        var listCriteria = QueryEntity<TEntity>(node, session, out var field)
            .Add(Restrictions.Eq($"{field}.{nameof(IEntity.Id)}", joinId))
            .SetMaxResults(5);

        return listCriteria.Future<TEntity>();
    }

    public ICriteria QueryEntity<TEntity>(INode node, ISession session, out string field)
        where TEntity : class, IEntity
    {
        field = GetUniqueName<TEntity>(nameof(QueryEntity));

        var criteria = session.CreateCriteria<TEntity>(field);

        while (node is JoinNode join && node.Parent is TableNode parent)
        {
            var parentKey = $"{field}.{join.QueryJoin.ChildProperty}";

            (criteria, field) = parent.Joiner(parent.Guid, criteria, parentKey, join.QueryJoin.ParentProperty);

            node = parent;
        }

        return criteria;
    }

    public static ICriteria QueryMaster(ISession session, Query query, MasterNode node, bool count = false)
    {
        var (criteria, parentField) = node.Creator(session);

        var fields = new Dictionary<Guid, string> { { node.Guid, parentField } };

        criteria = Traverse(criteria, node, parentField, fields);

        foreach (var queryCondition in query.Conditions.OrderBy(c => c.Priority)) criteria.Add(queryCondition, fields);

        var property = $"{parentField}.{nameof(IEntity.Id)}";

        if (count) criteria.SetProjection(Projections.CountDistinct(property));

        else
        {
            criteria = criteria.SetProjection(Projections.GroupProperty(property));

            if (!query.Orders.Any()) criteria.AddOrder(Order.Asc(property));

            else foreach(var order in query.Orders.OrderBy(o => o.Priority)) criteria.AddOrder(order, fields);

            criteria.SetFirstResult(query.Skip);

            criteria.SetMaxResults(query.Take);
        }

        return criteria;
    }

    private static ICriteria Traverse(ICriteria criteria, INode node, string parentField, IDictionary<Guid, string> fields)
    {
        foreach (var child in node.Descendants.OfType<JoinNode>())
        {
            var parentKey = $"{parentField}.{child.QueryJoin.ParentProperty}";

            string childField;

            (criteria, childField) = child.Joiner(child.Guid, criteria, parentKey, child.QueryJoin.ChildProperty);

            fields[child.Guid] = childField;

            criteria = Traverse(criteria, child, childField, fields);
        }

        return criteria;
    }
}