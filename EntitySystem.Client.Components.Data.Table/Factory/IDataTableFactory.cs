using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Table.Parameters;

namespace EntitySystem.Client.Components.Data.Table.Factory;

public interface IDataTableFactory : IRegistrable
{
    IRenderer Build<TEntity>(DataTableParameters<TEntity> parameters);
}