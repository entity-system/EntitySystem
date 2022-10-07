using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Parameters;
using EntitySystem.Client.Components.Data.Record.List.Condition;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.ConditionLabel;

public class DataRecordListConditionParameters<TKey> : Featured, IParameters
{
    public BaseDataRecordList<TKey> RecordList { get; }

    public IDataRecordListCondition Condition { get; }

    public DataRecordListConditionParameters(BaseDataRecordList<TKey> recordList, IDataRecordListCondition condition)
    {
        RecordList = recordList;
        Condition = condition;
    }
}