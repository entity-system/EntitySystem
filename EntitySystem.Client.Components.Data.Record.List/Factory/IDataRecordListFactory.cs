using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Record.List.Parameters;

namespace EntitySystem.Client.Components.Data.Record.List.Factory;

public interface IDataRecordListFactory : IRegistrable
{
    IRenderer Build<TKey>(DataRecordListParameters<TKey> parameters);
}