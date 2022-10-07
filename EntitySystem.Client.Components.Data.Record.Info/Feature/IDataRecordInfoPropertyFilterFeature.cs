using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Components.Data.Record.Info.Property;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature;

public interface IDataRecordInfoPropertyFilterFeature : IFeature
{
    bool Filter<TKey>(BaseDataRecordInfo<TKey> recordList, IDataRecordInfoProperty property);
}