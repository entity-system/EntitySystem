using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Parameters;
using EntitySystem.Client.Components.Data.Record.List.Item;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.SelectBox;

public class DataRecordListSelectBoxParameters<TKey> : Featured, IParameters
{
    public BaseDataRecordList<TKey> RecordList { get; }

    public IDataRecordListItem<TKey> Record { get; }

    public int Index { get; }

    public DataRecordListSelectBoxParameters(BaseDataRecordList<TKey> recordList, IDataRecordListItem<TKey> record, int index)
    {
        RecordList = recordList;
        Record = record;
        Index = index;
    }
}