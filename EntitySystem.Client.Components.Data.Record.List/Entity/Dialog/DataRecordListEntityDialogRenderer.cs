using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Record.List.Entity.Dialog;

public class DataRecordListEntityDialogRenderer<TEntity, TComponent> : Renderer<BaseDataRecordList<TEntity>, TComponent>
    where TComponent : BaseDataRecordListEntityDialog<TEntity>
{
    public DataRecordListEntityDialogRenderer(BaseDataRecordList<TEntity> parameters) : base(parameters)
    {
    }
}