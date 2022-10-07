using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Header.Feature;
using EntitySystem.Client.Components.Data.Header.Parameters;
using EntitySystem.Client.Components.Data.Header.Property;

namespace EntitySystem.Client.Components.Data.Header.Factory;

public class DataHeaderFactory : IDataHeaderFactory
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataHeaderFactory(IRegistrationProvider registrationProvider)
    {
        _registrationProvider = registrationProvider;
    }

    public IRenderer Build<TProperty, TEntity, TValue>(DataHeaderParameters<TProperty, TEntity, TValue> parameters)
        where TProperty : IDataHeaderProperty<TEntity, TValue>
    {
        foreach (var processor in _registrationProvider.GetRegistrationsOf<IDataHeaderFeatureProcessor>())
        {
            processor.Process(parameters);
        }

        return new Renderer<IDataHeaderParameters<TProperty, TEntity, TValue>, DataHeader<TProperty, TEntity, TValue>>(parameters, parameters.Property.Priority, parameters.Property?.Name);
    }
}