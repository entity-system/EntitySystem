using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Output.Feature;
using EntitySystem.Client.Components.Data.Output.Parameters;

namespace EntitySystem.Client.Components.Data.Output.Factory;

public class DataOutputFactory : IDataOutputFactory
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataOutputFactory(IRegistrationProvider registrationProvider)
    {
        _registrationProvider = registrationProvider;
    }

    public IRenderer Build<TKey, TEntity, TValue>(DataOutputParameters<TKey, TEntity, TValue> parameters)
    {
        foreach (var processor in _registrationProvider.GetRegistrationsOf<IDataOutputFeatureProcessor>())
        {
            processor.Process(parameters);
        }

        return parameters.Build<DataOutput<TKey, TEntity, TValue>>();
    }
}