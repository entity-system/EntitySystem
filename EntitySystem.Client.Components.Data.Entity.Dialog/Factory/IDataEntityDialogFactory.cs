using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Entity.Dialog.Parameters;

namespace EntitySystem.Client.Components.Data.Entity.Dialog.Factory;

public interface IDataEntityDialogFactory : IRegistrable
{
    IRenderer Build<TEntity>(DataEntityDialogParameters<TEntity> parameters);
}