using System.Threading.Tasks;
using EntitySystem.Client.Components.Data.Entity.Dialog.Source;

namespace EntitySystem.Client.Components.Data.Entity.Dialog.Options;

public interface IDataEntityDialogOptions<TEntity>
{
    IEntityDialogSource<TEntity> GetEntityDialogSource();

    TEntity Entity { get; }

    Task<TEntity> CreateEntityAsync();

    Task OnCloseAsync();

    Task OnAfterUpdateAsync();

    Task OnAfterSaveAsync(TEntity entity);
}