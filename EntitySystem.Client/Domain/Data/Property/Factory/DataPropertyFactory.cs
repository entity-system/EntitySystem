using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Domain.Data.Property.Feature;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Property.Factory;

public class DataPropertyFactory : IDataPropertyFactory
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataPropertyFactory(IRegistrationProvider registrationProvider)
    {
        _registrationProvider = registrationProvider;
    }

    public void Process<TEntity, TValue>(DataProperty<TEntity, TValue> property)
        where TEntity : IEntity
    {
        foreach (var processor in _registrationProvider.GetRegistrationsOf<IDataPropertyFeatureProcessor>())
        {
            processor.Process(property);
        }
    }
}