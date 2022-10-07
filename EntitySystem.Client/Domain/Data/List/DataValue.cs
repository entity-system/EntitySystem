using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Output.Factory;
using EntitySystem.Client.Components.Data.Output.Object;
using EntitySystem.Client.Components.Data.Output.Options;
using EntitySystem.Client.Components.Data.Output.Parameters;
using EntitySystem.Client.Components.Data.Output.Property;
using EntitySystem.Client.Components.Data.Record.List.Property;
using EntitySystem.Client.Domain.Data.Property;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.List;

public class DataValue<TKey, TEntity, TValue> : Featured, IDataValue<TKey, TEntity, TValue>
    where TKey : IEntity
    where TEntity : IEntity
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataObject<TKey, TEntity> Object { get; }

    public DataProperty<TEntity, TValue> Property { get; }

    public DataValue(DataObject<TKey, TEntity> o, DataProperty<TEntity, TValue> property)
    {
        _registrationProvider = o.Source.RegistrationProvider;

        Object = o;
        Property = property;
    }

    public IDataObject<TKey, TEntity> GetKeyEntitiesObject()
    {
        return Object;
    }

    public IDataProperty<TEntity, TValue> GetEntityValueProperty()
    {
        return Property;
    }

    public IDataProperty GetProperty()
    {
        return Property;
    }

    public IDataRecordListProperty GetRecordListProperty()
    {
        return Property;
    }

    public IRenderer BuildOutput(IDataOutputOptions options)
    {
        var parameters = new DataOutputParameters<TKey, TEntity, TValue>(this, options);

        var factory = _registrationProvider.GetRegistration<IDataOutputFactory>();

        return factory.Build(parameters);
    }

    public IDataOutputObject<TKey, TEntity> GetOutputObject()
    {
        return Object;
    }

    public IDataOutputProperty<TEntity, TValue> GetOutputProperty()
    {
        return Property;
    }
}