using EntitySystem.Client.Components.Data.Record.Info.Source;
using EntitySystem.Client.Components.Data.Table.Join.Record;

namespace EntitySystem.Client.Components.Data.Record.Info;

public interface IDataRecordInfo<TKey> :
    IDataTableJoinRecord<TKey>
{
    IDataRecordInfoSource<TKey> GetRecordInfoSource();
}