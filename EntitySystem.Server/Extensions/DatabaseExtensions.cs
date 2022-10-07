using System;
using System.Collections.Generic;
using System.Linq;
using EntitySystem.Server.Exceptions;
using EntitySystem.Shared.Domain;
using EntitySystem.Shared.Exceptions;
using EntitySystem.Shared.Query;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Impl;
using NHibernate.Loader.Criteria;
using NHibernate.Persister.Entity;
using NHibernate.SqlCommand;

namespace EntitySystem.Server.Extensions;

public static class DatabaseExtensions
{
    public static string ToSqlString(this ICriteria criteria)
    {
        var criteriaImpl = criteria as CriteriaImpl;

        var sessionImpl = criteriaImpl?.Session ?? throw new GeneralFriendlyException("Null session.");
            
        var factory = sessionImpl.Factory;
            
        var implementors = factory.GetImplementors(criteriaImpl.EntityOrClassName);
            
        var loader = new CriteriaLoader(factory.GetEntityPersister(implementors[0]) as IOuterJoinLoadable, factory, criteriaImpl, implementors[0], sessionImpl.EnabledFilters);

        return loader.SqlString.ToString();
    }

    public static ICriteria CreateKey(this ICriteria criteria, string field, string property, out string key)
    {
        return ReduceKey(criteria, $"{field}.{property}", out key);
    }

    public static ICriteria ReduceKey(this ICriteria criteria, string inputKey, out string outputKey)
    {
        var inputPath = inputKey.Split('.');

        outputKey = inputPath.Length <= 2 ? inputKey : $"{CreateAlias(inputPath.SkipLast(1))}.{inputPath.Last()}";

        return criteria;

        string CreateAlias(IEnumerable<string> path) => path.Select(acc => (acc, "Alias")).Aggregate((current, next) =>
        {
            var (currentPart, currentName) = current;

            var (nextPart, _) = next;

            var name = $"{currentName}{nextPart}";

            var alias = $"{name}{Guid.NewGuid().ToSqlAlias()}";

            criteria = criteria.CreateAlias($"{currentPart}.{nextPart}", alias, JoinType.LeftOuterJoin);

            return (alias, name);

        }).acc;
    }

    public static ICriteria Add(this ICriteria criteria, QueryCondition queryCondition, IDictionary<Guid, string> fields)
    {
        var condition = queryCondition.GetCondition();

        var keys = condition.GetParameters().OfType<QueryTarget>().Select(PrepareKey).ToList();

        var constants = condition.GetParameters().OfType<QueryConstant>().Select(c => c.GetValue()).ToList();

        var restriction = condition switch
        {
            QueryEqual when keys.Count == 1 && constants.Count == 1 && constants.All(c => c == null) => Restrictions.IsNull(keys.Single()),
            QueryEqual when keys.Count == 1 && constants.Count == 1 => Restrictions.Eq(keys.Single(), constants.Single()),
            QueryEqual when keys.Count == 2 && !constants.Any() => Restrictions.EqProperty(keys.Single(), keys.Single()),
            QueryLike when keys.Count == 1 && constants.Count == 1 => Restrictions.InsensitiveLike(keys.Single(), $"%{constants.Single()}%"),
            _ => throw new GeneralFriendlyException("Unsupported queryCondition.")
        };

        return criteria.Add(restriction);

        string PrepareKey(QueryTarget target)
        {
            criteria = criteria.CreateKey(fields[target.Guid], target.Property, out var key);

            return key;
        }
    }

    public static ICriteria AddOrder(this ICriteria criteria, QueryOrder queryOrder, IDictionary<Guid, string> fields)
    {
        var value = queryOrder.GetOrder();

        var order = value switch
        {
            QueryOrderAscending asc => Order.Asc(CreateMinKey(asc.Target)),
            QueryOrderDescending desc => Order.Desc(CreateMinKey(desc.Target)),
            _ => throw new GeneralFriendlyException("Unsupported queryOrder.")
        };

        criteria.AddOrder(order.IgnoreCase());

        return criteria;

        IProjection CreateMinKey(QueryTarget target)
        {
            criteria = criteria.CreateKey(fields[target.Guid], target.Property, out var key);

            return Projections.Min(key);
        }
    }

    public static ISession EnsureExist<TEntity, TValue>(this ISession session, IInitial<TEntity> initial, Func<TEntity, TValue> key, Action<TEntity> mutate = null)
        where TEntity : IEntity
    {
        using var transaction = session.BeginTransaction();

        var existing = session.Query<TEntity>().ToDictionary(key, e => e);

        foreach (var info in initial.GetAccessors().Select(a => new { Original = a.getter(), Access = a }))
        {
            if (existing.TryGetValue(key(info.Original), out var entity)) info.Access.setter(entity);

            else
            {
                mutate?.Invoke(info.Original);

                session.SaveOrUpdate(info.Original);
            }
        }

        transaction.Commit();

        return session;
    }
}