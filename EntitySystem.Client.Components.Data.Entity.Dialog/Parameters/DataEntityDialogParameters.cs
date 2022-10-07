using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Components.Data.Entity.Dialog.Options;

namespace EntitySystem.Client.Components.Data.Entity.Dialog.Parameters;

public class DataEntityDialogParameters<TEntity> : Featured, IDataEntityDialogParameters<TEntity>
{
    public IDataEntityDialogOptions<TEntity> Options { get; }

    public DataEntityDialogParameters(IDataEntityDialogOptions<TEntity> options)
    {
        Options = options;
    }
}