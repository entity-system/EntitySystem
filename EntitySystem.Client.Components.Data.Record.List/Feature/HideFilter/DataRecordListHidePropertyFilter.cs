using EntitySystem.Client.Components.Data.Record.List.Property;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.HideFilter;

public class DataRecordListHidePropertyFilter : IDataRecordListFilterFeature
{
    public bool IsHidden<TKey>(BaseDataRecordList<TKey> recordList, IDataRecordListProperty property)
    {
        return true;
    }
}