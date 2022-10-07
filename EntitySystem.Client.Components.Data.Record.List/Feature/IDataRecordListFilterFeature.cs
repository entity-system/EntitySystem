using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Components.Data.Record.List.Property;

namespace EntitySystem.Client.Components.Data.Record.List.Feature;

public interface IDataRecordListFilterFeature : IFeature
{
    bool IsHidden<TKey>(BaseDataRecordList<TKey> recordList, IDataRecordListProperty property);
}