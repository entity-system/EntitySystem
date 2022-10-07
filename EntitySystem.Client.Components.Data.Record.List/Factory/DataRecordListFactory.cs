using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Record.List.Feature;
using EntitySystem.Client.Components.Data.Record.List.Parameters;

namespace EntitySystem.Client.Components.Data.Record.List.Factory;

public class DataRecordListFactory : IDataRecordListFactory
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataRecordListFactory(IRegistrationProvider registrationProvider)
    {
        _registrationProvider = registrationProvider;
    }

    public IRenderer Build<TKey>(DataRecordListParameters<TKey> parameters)
    {
        foreach (var processor in _registrationProvider.GetRegistrationsOf<IDataRecordListFeatureProcessor>())
        {
            processor.Process(parameters);
        }

        return parameters.Build<DataRecordList<TKey>>();
    }
}