using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Components.Data.Entity.Dialog.Parameters;

namespace EntitySystem.Client.Components.Data.Entity.Dialog.Feature;

public interface IDataEntityDialogFeatureProcessor : IRegistrable
{
    void Process<TEntity>(IDataEntityDialogParameters<TEntity> parameters);
}