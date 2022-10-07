using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Components.Data.Record.Info.Parameters;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature;

public interface IDataRecordInfoFeatureProcessor : IRegistrable
{
    void Process<TKey>(DataRecordInfoParameters<TKey> parameters);
}