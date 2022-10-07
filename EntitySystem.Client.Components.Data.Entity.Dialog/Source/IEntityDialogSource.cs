using System.Collections.Generic;
using System.Threading.Tasks;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Entity.Dialog.Options;
using EntitySystem.Client.Components.Data.Entity.Dialog.Property;

namespace EntitySystem.Client.Components.Data.Entity.Dialog.Source;

public interface IEntityDialogSource : IFeatured
{
    string Name { get; }

    void ResetProperties();

  
}

public interface IEntityDialogSource<TEntity> : IEntityDialogSource
{
    IRenderer BuildEntityDialog(IDataEntityDialogOptions<TEntity> options);

    Task DeleteAsync(params TEntity[] entities);
    IEnumerable<IDataEntityDialogProperty<TEntity>> GetEntityDialogProperties();

    Task<TEntity> CreateEntityAsync();

    Task InitializeEntityAsync(TEntity entity);

    bool IsNew(TEntity entity);

    Task<TEntity> SaveOrUpdateAsync(TEntity entity);
}