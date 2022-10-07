using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Record.Info.Join;
using EntitySystem.Client.Components.Data.Record.Info.Source;
using EntitySystem.Client.Components.Data.Table.Join;
using EntitySystem.Client.Components.Data.Table.Join.Factory;
using EntitySystem.Client.Components.Data.Table.Join.Options;
using EntitySystem.Client.Components.Data.Table.Join.Parameters;
using EntitySystem.Client.Components.Data.Table.Join.Record;
using EntitySystem.Client.Components.Data.Table.Join.Source;
using EntitySystem.Client.Domain.Data.Condition;
using EntitySystem.Client.Domain.Data.List;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Client.Domain.Data.Target.Feature.PropagatedEntity;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Abstract.Extensions;
using EntitySystem.Shared.Domain;
using EntitySystem.Shared.Query;

namespace EntitySystem.Client.Domain.Data.Join;

public class DataJoin<TParent, TChild, TMiddle, TParentService, TChildService> : Featured, IDataJoin<TParent, TChild>,
    IDataRecordInfoJoin<TParent>,
    IDataTableJoin<TParent, TChild>
    where TParent : IEntity, new()
    where TChild : IEntity, new()
    where TMiddle : IEntity
    where TParentService : IEntityService<TParent>
    where TChildService : IEntityService<TChild>
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataSource<TParent, TParentService> ParentSource { get; }

    public DataSource<TChild, TChildService> ChildSource { get; }

    public Expression<Func<TParent, TMiddle>> ParentMiddleExpression { get; }

    public Func<TParent, TMiddle> ParentMiddleGetter { get; }

    public Expression<Func<TChild, TMiddle>> ChildMiddleExpression { get; }

    public Func<TChild, TMiddle> ChildMiddleGetter { get; }

    public Action<TChild, TMiddle> ChildMiddleSetter { get; }

    public Dictionary<TMiddle, TChild[]> Cache { get; private set; }

    public DataJoin(DataSource<TParent, TParentService> parentSource, DataSource<TChild, TChildService> childSource, Expression<Func<TParent, TMiddle>> parentMiddleExpression, Expression<Func<TChild, TMiddle>> childMiddleExpression)
    {
        _registrationProvider = parentSource.RegistrationProvider;

        ParentSource = parentSource;

        ChildSource = childSource;

        ParentMiddleExpression = parentMiddleExpression;

        ParentMiddleGetter = parentMiddleExpression.Compile();

        ChildMiddleExpression = childMiddleExpression;

        ChildMiddleGetter = childMiddleExpression.Compile();

        ChildMiddleSetter = childMiddleExpression.CompileSetter();
    }

    public IDataSource<TParent> GetParentEntitySource()
    {
        return ParentSource;
    }

    public IDataSource<TChild> GetChildEntitySource()
    {
        return ChildSource;
    }

    public IDataSource GetChildSource()
    {
        return ChildSource;
    }

    public async Task RequestNestedAsync()
    {
        var queryJoin = new QueryJoin
        {
            ParentProperty = QueryTarget.ToString(ParentMiddleExpression.Body),
            ChildProperty = QueryTarget.ToString(ChildMiddleExpression.Body)
        };

        ChildSource.Guid = await ChildSource.EntityService.JoinAsync(ParentSource.Guid, queryJoin);

        foreach (var join in ChildSource.Joins) await join.RequestNestedAsync();
    }

    public IEnumerable<IDataObject<TKey>> CreateObjectsNested<TKey>(DataRecord<TKey> record, IEnumerable<TParent> parents) where TKey : IEntity
    {
        Cache = ChildSource.List.GroupBy(ChildMiddleGetter, t => t).ToDictionary(g => g.Key, g => g.ToArray());

        var children = parents.SelectMany(s => Cache.TryGetValue(ParentMiddleGetter(s), out var entities) ? entities : Array.Empty<TChild>()).ToArray();

        return ChildSource.CreateObjectsNested(record, null, children);
    }

    public IDataRecordInfoJoin<TParent> GetRecordInfoJoin()
    {
        return this;
    }

    public IDataRecordInfoSource GetRecordInfoChildSource()
    {
        return ChildSource;
    }

    public IRenderer BuildTableJoin(IDataTableJoinOptions<TParent> options)
    {
        var parentMiddle = ParentMiddleGetter(options.GetJoinRecord().Key);

        var condition = new DataCondition<TChild>(ChildMiddleExpression.Compose(v => v.Id == parentMiddle.Id));

        condition.Assign(ChildSource);

        var parameters = new DataTableJoinParameters<TParent, TChild>(this, options);

        var factory = _registrationProvider.GetRegistration<IDataTableJoinFactory>();

        return factory.Build(parameters);
    }

    public IDataTableJoinSource<TChild> GetTableJoinChildSource()
    {
        return ChildSource;
    }

    public async Task RequestChildrenAsync(TParent parent)
    {
        await ChildSource.RequestNestedAsync();
    }

    public IEnumerable<IDataTableJoinRecord<TChild>> CreateTableJoinChildRecords()
    {
        return ChildSource.CreateTableRecordsNested();
    }

    public async Task OnAfterCreateChildAsync(TParent parent, TChild child)
    {
        ChildMiddleSetter(child, ParentMiddleGetter(parent));

        foreach (var feature in Features.GetFeatures<IDataPropagatedEntityFeature>())
        {
            await feature.OnAfterCreateChildAsync(this, parent, child);
        }

        await Task.CompletedTask;
    }

    public IEnumerable<IEntity> GetPairedEntities(IEntity entity)
    {
        if (entity is not TParent source || Cache == null || !Cache.TryGetValue(ParentMiddleGetter(source), out var targets)) yield break;

        foreach (var target in targets) yield return target;
    }
}