using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Components.Data.Entity.Dialog.Property;

namespace EntitySystem.Client.Components.Data.Entity.Dialog.Feature;

public interface IDataEntityDialogPropertyFilter : IFeature
{
    bool IsHidden<TEntity>(BaseDataEntityDialog<TEntity> dialog, IDataEntityDialogProperty<TEntity> property);
}