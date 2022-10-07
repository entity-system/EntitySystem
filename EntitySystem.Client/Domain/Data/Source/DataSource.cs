using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Entity.Dialog.Factory;
using EntitySystem.Client.Components.Data.Entity.Dialog.Options;
using EntitySystem.Client.Components.Data.Entity.Dialog.Parameters;
using EntitySystem.Client.Components.Data.Entity.Dialog.Property;
using EntitySystem.Client.Components.Data.Header.Source;
using EntitySystem.Client.Components.Data.Input.Target.Source;
using EntitySystem.Client.Components.Data.Output.Source;
using EntitySystem.Client.Components.Data.Record.Info.Factory;
using EntitySystem.Client.Components.Data.Record.Info.Join;
using EntitySystem.Client.Components.Data.Record.Info.Options;
using EntitySystem.Client.Components.Data.Record.Info.Parameters;
using EntitySystem.Client.Components.Data.Record.Info.Property;
using EntitySystem.Client.Components.Data.Record.Info.Source;
using EntitySystem.Client.Components.Data.Record.List.Condition;
using EntitySystem.Client.Components.Data.Record.List.Factory;
using EntitySystem.Client.Components.Data.Record.List.Feature.Title;
using EntitySystem.Client.Components.Data.Record.List.Options;
using EntitySystem.Client.Components.Data.Record.List.Order;
using EntitySystem.Client.Components.Data.Record.List.Parameters;
using EntitySystem.Client.Components.Data.Record.List.Property;
using EntitySystem.Client.Components.Data.Table.Factory;
using EntitySystem.Client.Components.Data.Table.Join.Source;
using EntitySystem.Client.Components.Data.Table.Options;
using EntitySystem.Client.Components.Data.Table.Parameters;
using EntitySystem.Client.Components.Data.Table.Record;
using EntitySystem.Client.Components.Data.Table.Source;
using EntitySystem.Client.Domain.Data.Condition;
using EntitySystem.Client.Domain.Data.Join;
using EntitySystem.Client.Domain.Data.Join.Factory;
using EntitySystem.Client.Domain.Data.List;
using EntitySystem.Client.Domain.Data.Order;
using EntitySystem.Client.Domain.Data.Pair;
using EntitySystem.Client.Domain.Data.Property;
using EntitySystem.Client.Domain.Data.Target;
using EntitySystem.Client.Domain.Data.Target.Factory;
using EntitySystem.Client.Domain.Data.Target.Feature.PropagatedEntity;
using EntitySystem.Client.Extensions;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;
using EntitySystem.Shared.Extensions;
using EntitySystem.Shared.Query;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace EntitySystem.Client.Domain.Data.Source;

public class DataSource<TEntity, TService> : Featured, IDataSource<TEntity>,
    IDataTableSource<TEntity>,
    IDataTableJoinSource<TEntity>,
    IDataHeaderSource<TEntity>,
    IDataInputTargetSource<TEntity>,
    IDataOutputSource<TEntity>
    where TEntity : IEntity, new()
    where TService : IEntityService<TEntity>
{
    public IRegistrationProvider RegistrationProvider { get; }

    public IDataService DataService { get; }

    public TService EntityService { get; set; }

    public Guid Guid { get; set; }

    public string Name { get; set; }

    public int TargetDeep { get; set; }

    public int JoinDeep { get; set; }

    public List<IDataProperty<TEntity>> Properties { get; set; } = new();

    public List<IDataTarget<TEntity>> Targets { get; set; } = new();

    public List<IDataJoin<TEntity>> Joins { get; set; } = new();

    public List<DataCondition<TEntity>> Conditions { get; set; } = new();

    public List<IDataOrder<TEntity>> Orders { get; set; } = new();

    public int Limit { get; set; } = 10;

    public int Offset { get; set; }

    public int MasterCount { get; set; }

    public List<TEntity> List { get; set; } = new();

    public IDataSource Cloned { get; set; }

    public Action<DataSource<TEntity, TService>> HintOptions { get; set; }

    public DataSource(IServiceProvider serviceProvider, string name, int targetDeep, int joinDeep)
    {
        RegistrationProvider = serviceProvider.GetService<IRegistrationProvider>();

        DataService = serviceProvider.GetService<IDataService>();

        EntityService = serviceProvider.GetService<TService>();

        Name = name;

        TargetDeep = targetDeep;

        JoinDeep = joinDeep;
    }

    public DataSource<TEntity, TService> Target<TChild, TChildService>(string type, Expression<Func<TEntity, TChild>> property, Expression<Func<TChild, string>> name, Action<DataSource<TChild, TChildService>> childSourceOptions = null, Action<DataTarget<TEntity, TService, TChild, TChildService>> targetOptions = null)
        where TChild : IEntity, new()
        where TChildService : IEntityService<TChild>
    {
        var childSource = DataService.Get<TChild, TChildService>(type, TargetDeep + 1, JoinDeep);

        childSourceOptions?.Invoke(childSource);

        var factory = RegistrationProvider.GetRegistration<IDataTargetFactory>();

        var dataTarget = factory.Build(this, childSource, property, name);

        targetOptions?.Invoke(dataTarget);

        Targets.Add(dataTarget);

        return this;
    }

    public DataSource<TEntity, TService> Join<TChild, TChildService, TMiddle>(string type, Expression<Func<TEntity, TMiddle>> sourceSelector, Expression<Func<TChild, TMiddle>> targetSelector, Action<DataSource<TChild, TChildService>> options = null)
        where TChild : IEntity, new()
        where TChildService : IEntityService<TChild>
        where TMiddle : IEntity
    {
        return Join(type, sourceSelector, targetSelector, out _, options);
    }

    public DataSource<TEntity, TService> Join<TChild, TChildService, TMiddle>(string type, Expression<Func<TEntity, TMiddle>> parentMiddleSelector, Expression<Func<TChild, TMiddle>> childMiddleSelector, out DataSource<TChild, TChildService> childSource, Action<DataSource<TChild, TChildService>> options = null)
        where TChild : IEntity, new()
        where TChildService : IEntityService<TChild>
        where TMiddle : IEntity
    {
        childSource = DataService.Get<TChild, TChildService>(type, TargetDeep, JoinDeep + 1);

        childSource.Features.AddFeature<DataRecordListDisableTitleFeature>();

        options?.Invoke(childSource);

        var factory = RegistrationProvider.GetRegistration<IDataJoinFactory>();

        var join = factory.Build(this, childSource, parentMiddleSelector, childMiddleSelector);

        Joins.Add(join);

        return this;
    }

    public async Task<TEntity> SaveOrUpdateAsync(TEntity entity)
    {
        foreach (var target in Targets) await target.OnBeforeSaveOrUpdateAsync(entity);

        var result = await EntityService.PutAsync(entity);

        foreach (var property in Properties) await property.OnAfterSaveOrUpdateAsync(result);

        return result;
    }

    public async Task DeleteAsync(params TEntity[] entities)
    {
        await EntityService.DeleteAsync(entities);
    }

    public void AddProperty(IDataProperty<TEntity> property)
    {
        property.JoinDeep = JoinDeep;

        property.TargetDeep = TargetDeep;

        Properties.Add(property);
    }

    public void AddCondition(Expression<Func<TEntity, bool>> equals, string name)
    {
        var condition = new DataCondition<TEntity>(equals, name);

        condition.Assign(this);
    }

    public void AddOrder<TValue>(Expression<Func<TEntity, TValue>> selector, bool descending, string name)
    {
        var order = new DataOrder<TEntity, TValue>(selector, descending, name);

        order.Assign(this);
    }

    public void Hints(Action<DataSource<TEntity, TService>> options)
    {
        HintOptions = options;
    }

    public async Task<List<TEntity>> GetTargetHintsAsync(Expression<Func<TEntity, bool>> expression = null)
    {
        return await SearchAsync(expression, HintOptions);
    }

    public async Task<List<TEntity>> GetFilterHintsAsync(Expression<Func<TEntity, bool>> expression = null)
    {
        return await SearchAsync(expression);
    }

    private async Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> expression = null, Action<DataSource<TEntity, TService>> options = null)
    {
        var source = Clone();

        if (expression != null)
        {
            var condition = new DataCondition<TEntity>(expression) { Like = true };

            condition.Assign(source);
        }

        options?.Invoke(source);

        await source.RequestNestedAsync();

        return source.List;
    }

    public DataSource<TEntity, TService> Clone()
    {
        var cloned = DataService.Get<TEntity, TService>(Name);

        cloned.Cloned = this;

        return cloned;
    }

    public RenderFragment Render(IDataTableOptions options)
    {
        if(options.Id > 0) AddCondition(options.Id);

        return BuildTable(options).Render();
    }

    public IRenderer BuildTable(IDataTableOptions options)
    {
        var parameters = new DataTableParameters<TEntity>(this, options);

        var factory = RegistrationProvider.GetRegistration<IDataTableFactory>();

        return factory.Build(parameters);
    }

    public async Task RequestNestedAsync()
    {
        await QueryAsync();

        foreach (var join in Joins) await join.RequestNestedAsync();

        var query = CreateQueryNested();

        await ListNestedAsync(query);
    }

    public async Task RequestAsync()
    {
        await QueryAsync();

        var query = CreateQuery();

        await ListAsync(query);
    }

    private async Task QueryAsync()
    {
        Guid = await EntityService.QueryAsync();
    }

    public Query CreateQueryNested()
    {
        var query = CreateDefaultQuery();

        FillQueryNested(query);

        return query;
    }

    public Query CreateQuery()
    {
        var query = CreateDefaultQuery();

        FillQuery(query);

        return query;
    }

    private Query CreateDefaultQuery()
    {
        return new Query
        {
            Take = Limit,
            Skip = Offset
        };
    }

    public void FillQueryNested(Query query)
    {
        FillQuery(query);

        Joins.ForEach(j => j.GetChildSource().FillQueryNested(query));
    }

    public void FillQuery(Query query)
    {
        foreach (var condition in Conditions)
        {
            query.Conditions.Add(condition.GetQueryCondition());
        }

        foreach (var order in Orders)
        {
            query.Orders.Add(order.GetQueryOrder());
        }
    }

    public async Task ListNestedAsync(Query query)
    {
        await ListAsync(query);

        foreach (var join in Joins) await join.GetChildSource().ListNestedAsync(query);
    }

    public async Task ListAsync(Query query)
    {
        var pageList = await EntityService.ListAsync(Guid, query);

        List = pageList.Page;

        foreach (var entity in List) await InitializeEntityAsync(entity);

        MasterCount = pageList.MasterCount;
    }

    public void AddCondition(long id)
    {
        var condition = new DataCondition<TEntity>(e => e.Id == id);

        condition.Assign(this);
    }

    public IEnumerable<IDataTableRecord<TEntity>> CreateTableRecordsNested()
    {
        return List.Select<TEntity, IDataTableRecord<TEntity>>(e =>
        {
            var record = new DataRecord<TEntity>(this, e);

            record.Objects = CreateObjectsNested(record, o => record.KeyObject = o, e).ToArray();

            return record;
        });
    }

    public IEnumerable<IDataTableRecord<TEntity>> CreateTableRecords()
    {
        return List.Select<TEntity, IDataTableRecord<TEntity>>(e =>
        {
            var record = new DataRecord<TEntity>(this, e);

            record.KeyObject = CreateObject(record, e);

            record.Objects = new[] { record.KeyObject };

            return record;
        });
    }

    public IEnumerable<IDataObject<TKey>> CreateObjectsNested<TKey>(DataRecord<TKey> record, Action<DataObject<TKey, TEntity>> keyObjectOptions = null, params TEntity[] entities)
        where TKey : IEntity
    {
        var keyObject = CreateObject(record, entities);

        keyObjectOptions?.Invoke(keyObject);

        yield return keyObject;

        foreach (var @object in Joins.SelectMany(join => join.CreateObjectsNested(record, entities)))
            yield return @object;
    }

    public DataObject<TKey, TEntity> CreateObject<TKey>(DataRecord<TKey> record, params TEntity[] entities)
        where TKey : IEntity
    {
        return new DataObject<TKey, TEntity>(this, record, entities, GetObjectPropertiesNested().ToArray());
    }

    public IEnumerable<IDataProperty<TEntity>> GetObjectPropertiesNested()
    {
        foreach (var property in Properties) yield return property;

        foreach (var property in Targets.SelectMany(t => t.GetObjectPropertiesNested())) yield return property;
    }

    public IEnumerable<IDataProperty> GetAllPropertiesInRelationWith(params Type[] types)
    {
        return GetAllProperties().Where(p => types.Any(p.IsInRelationWith));
    }

    public IEnumerable<IDataProperty> GetAllProperties()
    {
        foreach (var property in Properties) yield return property;

        foreach (var property in Targets) yield return property;

        foreach (var property in Targets.SelectMany(t => t.GetChildSource().GetAllProperties())) yield return property;

        foreach (var property in Targets.SelectMany(t => t.GetObjectPropertiesNested())) yield return property;

        foreach (var property in Joins.SelectMany(j => j.GetChildSource().GetAllProperties())) yield return property;
    }

    public IRenderer BuildRecordInfo<TKey>(IDataRecordInfoOptions<TKey> options)
    {
        var parameters = new DataRecordInfoParameters<TKey>(options);

        var factory = RegistrationProvider.GetRegistration<IDataRecordInfoFactory>();

        return factory.Build(parameters);
    }

    public IDataRecordInfoSource<TEntity> GetRecordInfoSource()
    {
        return this;
    }

    public IEnumerable<IDataRecordInfoProperty<TEntity>> GetRecordInfoProperties()
    {
        foreach (var property in Properties) yield return property.GetRecordInfoProperty();

        foreach (var property in Targets.SelectMany(t => t.GetRecordInfoProperties())) yield return property;
    }

    public IEnumerable<IDataRecordInfoJoin<TEntity>> GetRecordInfoJoins()
    {
        return Joins.Select(j => j.GetRecordInfoJoin());
    }

    public IRenderer BuildRecordList<TKey>(IDataRecordListOptions<TKey> options)
    {
        var parameters = new DataRecordListParameters<TKey>(options);

        var factory = RegistrationProvider.GetRegistration<IDataRecordListFactory>();

        return factory.Build(parameters);
    }

    public IEnumerable<IDataRecordListOrder> GetRecordListOrdersNested()
    {
        foreach (var order in Orders.Select(o => o.GetRecordListOrder())) yield return order;

        foreach (var order in Joins.SelectMany(j => j.GetChildSource().GetRecordListOrdersNested())) yield return order;
    }

    public IEnumerable<IDataRecordListCondition> GetRecordListConditionsNested()
    {
        foreach (var condition in Conditions) yield return condition;

        foreach (var condition in Joins.SelectMany(j => j.GetChildSource().GetRecordListConditionsNested())) yield return condition;
    }

    public IEnumerable<IDataRecordListProperty> GetRecordListProperties()
    {
        foreach (var property in Properties) yield return property.GetRecordListProperty();

        foreach (var property in Targets.SelectMany(t => t.GetRecordListProperties())) yield return property;

        foreach (var property in Joins.SelectMany(j => j.GetChildSource().GetRecordListProperties())) yield return property;
    }

    public IDataHeaderSource<TEntity> GetHeaderSource()
    {
        return this;
    }

    public Expression<Func<TEntity, bool>> GetIdEquation(TEntity entity)
    {
        return e => e.Id == entity.Id;
    }

    public IDataOutputSource<TEntity> GetOutputSource()
    {
        return this;
    }

    public IRenderer BuildEntityDialog(IDataEntityDialogOptions<TEntity> options)
    {
        var parameters = new DataEntityDialogParameters<TEntity>(options);

        var factory = RegistrationProvider.GetRegistration<IDataEntityDialogFactory>();

        return factory.Build(parameters);
    }

    public IEnumerable<IDataEntityDialogProperty<TEntity>> GetEntityDialogProperties()
    {
        foreach (var property in Properties) yield return property.GetEntityDialogProperty();

        foreach (var property in Targets.SelectMany(t => t.GetEntityDialogRawProperties())) yield return property;

        foreach (var property in Targets.SelectMany(t => t.GetEntityDialogTargetProperties())) yield return property.GetEntityDialogProperty();
    }

    public bool IsNew(TEntity entity)
    {
        return entity.IsNew();
    }

    public async Task<TEntity> CreateEntityAsync()
    {
        var entity = new TEntity();

        await InitializeEntityAsync(entity);

        return entity;
    }

    public async Task InitializeEntityAsync(TEntity entity)
    {
        foreach (var feature in Features.GetFeatures<IDataPropagatedEntityFeature>())
        {
            await feature.OnAfterInitializeEntityAsync(this, entity);
        }

        foreach (var property in Targets) await property.InitializeEntityAsync(entity);
    }

    public void ResetProperties()
    {
        foreach (var property in Properties) property.Reset();

        foreach (var target in Targets) target.ResetProperties();
    }

    public IEnumerable<IDataPair> GetPairs()
    {
        foreach (var target in Targets) yield return target;

        foreach (var join in Joins) yield return join;
    }
}