using EntitySystem.Client.Components.Data.Record.Info.Property;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature.HideFilter;

public class DataRecordInfoHidePropertyFilter : IDataRecordInfoPropertyFilterFeature
{
    public bool Filter<TKey>(BaseDataRecordInfo<TKey> recordInfo, IDataRecordInfoProperty property)
    {
        return false;
    }
}