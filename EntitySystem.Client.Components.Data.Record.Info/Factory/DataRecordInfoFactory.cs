using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Record.Info.Feature;
using EntitySystem.Client.Components.Data.Record.Info.Parameters;

namespace EntitySystem.Client.Components.Data.Record.Info.Factory;

public class DataRecordInfoFactory : IDataRecordInfoFactory
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataRecordInfoFactory(IRegistrationProvider registrationProvider)
    {
        _registrationProvider = registrationProvider;
    }

    public IRenderer Build<TKey>(DataRecordInfoParameters<TKey> parameters)
    {
        foreach (var processor in _registrationProvider.GetRegistrationsOf<IDataRecordInfoFeatureProcessor>())
        {
            processor.Process(parameters);
        }

        return parameters.Build<DataRecordInfo<TKey>>();
    }
}