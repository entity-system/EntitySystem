using EntitySystem.Client.Components.Data.Entity.Dialog.Property;

namespace EntitySystem.Client.Components.Data.Entity.Dialog.Feature.HidePropertyInEdit;

public class DataEntityDialogHidePropertyInEditFeature : IDataEntityDialogHidePropertyInEditFeature
{
    public bool IsHidden<TEntity>(BaseDataEntityDialog<TEntity> dialog, IDataEntityDialogProperty<TEntity> property)
    {
        return !dialog.IsNewEntity;
    }
}