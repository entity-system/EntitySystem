using EntitySystem.Client.Components.Data.Entity.Dialog.Property;

namespace EntitySystem.Client.Components.Data.Entity.Dialog.Feature.HidePropertyInAdd;

public class DataEntityDialogHidePropertyInAddFeature : IDataEntityDialogHidePropertyInAddFeature
{
    public bool IsHidden<TEntity>(BaseDataEntityDialog<TEntity> dialog, IDataEntityDialogProperty<TEntity> property)
    {
        return dialog.IsNewEntity;
    }
}