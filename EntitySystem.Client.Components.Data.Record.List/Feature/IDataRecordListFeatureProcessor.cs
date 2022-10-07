using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Components.Data.Record.List.Parameters;

namespace EntitySystem.Client.Components.Data.Record.List.Feature;

public interface IDataRecordListFeatureProcessor : IRegistrable
{
    public void Process<TEntity>(IDataRecordListParameters<TEntity> parameters);
}