using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Record.List.Item;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.Value;

public class DataRecordListValueFeature : IDataRecordListValueFeature
{
    public IEnumerable<IRenderer> BuildHeader<TKey>(BaseDataRecordList<TKey> recordList)
    {
        return recordList.Source.GetRecordListProperties()
            .Where(p => !p.Features.GetFeatures<IDataRecordListFilterFeature>().Any(f => f.IsHidden(recordList, p)))
            .OrderBy(p => p.Priority)
            .Select(p => p.BuildHeader(recordList));
    }

    public IEnumerable<IRenderer> BuildOutput<TKey>(BaseDataRecordList<TKey> recordList, IDataRecordListItem<TKey> record, int index)
    {
        return record.GetRecordListValues()
            .Select(v => new { Value = v, Property = v.GetRecordListProperty() })
            .Where(o => !o.Property.Features.GetFeatures<IDataRecordListFilterFeature>().Any(f => f.IsHidden(recordList, o.Property)))
            .OrderBy(o => o.Property.Priority)
            .Select(o => o.Value.BuildOutput(recordList));
    }
}