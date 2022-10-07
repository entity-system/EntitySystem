using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Table.Join.Parameters;

namespace EntitySystem.Client.Components.Data.Table.Join.Factory;

public interface IDataTableJoinFactory : IRegistrable
{
    IRenderer Build<TParent, TChild>(DataTableJoinParameters<TParent, TChild> parameters);
}