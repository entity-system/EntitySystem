using EntitySystem.Client.Components.Data.Record.List.Item;

namespace EntitySystem.Client.Components.Data.Table.Join.Record;

public interface IDataTableJoinRecord<out TEntity> : 
    IDataRecordListItem<TEntity>
{
}