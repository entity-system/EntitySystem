using System.Linq;
using EntitySystem.Client.Components.Data.Record.List.Property;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.DeepMaximumFilter;

public class DataRecordListHidePropertyByDeepMaximumFilter : IDataRecordListFilterFeature
{
    public int DeepMaximum { get; set; }

    public bool IsHidden<TKey>(BaseDataRecordList<TKey> recordList, IDataRecordListProperty property)
    {
        var currentDeep = new[] { property.TargetDeep - recordList.Source.TargetDeep, property.JoinDeep - recordList.Source.JoinDeep }.Max();

        return currentDeep > DeepMaximum;
    }
}