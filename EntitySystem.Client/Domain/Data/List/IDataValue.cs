using EntitySystem.Client.Components.Data.Output.Value;
using EntitySystem.Client.Components.Data.Record.List.Value;
using EntitySystem.Client.Domain.Data.Property;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.List;

public interface IDataValue<TKey, TEntity, TValue> : IDataValue<TKey>,
    IDataOutputValue<TKey, TEntity, TValue>
    where TKey : IEntity
    where TEntity : IEntity
{
    IDataObject<TKey, TEntity> GetKeyEntitiesObject();

    public IDataProperty<TEntity, TValue> GetEntityValueProperty();
}

public interface IDataValue<TKey> : IDataValue
{

    /*RenderFragment Render(EventCallback<RenderFragment> onEntityOpen = default, EventCallback<DataEntityAction<TKey>> onEntityAction = default);*/
}

public interface IDataValue : IDataRecordListValue
{
    IDataProperty GetProperty();
    /*RenderFragment Render(EventCallback<RenderFragment> onEntityOpen = default, EventCallback<DataEntityAction<TKey>> onEntityAction = default);*/
}