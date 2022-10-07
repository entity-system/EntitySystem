using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Parameters;
using EntitySystem.Client.Components.Data.Record.List.Order;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.OrderLabel;

public class DataRecordListOrderParameters<TKey> : Featured, IParameters
{
    public BaseDataRecordList<TKey> RecordList { get; }

    public IDataRecordListOrder Order { get; }

    public DataRecordListOrderParameters(BaseDataRecordList<TKey> recordList, IDataRecordListOrder order)
    {
        RecordList = recordList;
        Order = order;
    }
}