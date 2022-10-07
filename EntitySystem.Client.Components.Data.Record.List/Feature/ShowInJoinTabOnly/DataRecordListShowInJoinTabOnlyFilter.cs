using EntitySystem.Client.Components.Data.Record.List.Property;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.ShowInJoinTabOnly;

public class DataRecordListShowInJoinTabOnlyFilter : IDataRecordListFilterFeature
{
    public bool IsHidden<TKey>(BaseDataRecordList<TKey> recordList, IDataRecordListProperty property)
    {
        return recordList.Source.JoinDeep == 0;
    }
}