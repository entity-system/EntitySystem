using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Record.Info.Parameters;

namespace EntitySystem.Client.Components.Data.Record.Info.Factory;

public interface IDataRecordInfoFactory : IRegistrable
{
    IRenderer Build<TKey>(DataRecordInfoParameters<TKey> parameters);
}