using EntitySystem.Client.Abstract.Domain.Parameters;
using EntitySystem.Client.Components.Data.Entity.Dialog.Options;

namespace EntitySystem.Client.Components.Data.Entity.Dialog.Parameters;

public interface IDataEntityDialogParameters<TEntity> : IParameters
{
    IDataEntityDialogOptions<TEntity> Options { get; }
}