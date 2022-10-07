using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.OrderLabel;

internal class DataRecordListOrderLabelFeature : IDataRecordListOrderLabelFeature
{
    public const long Priority = 50;

    public IEnumerable<IRenderer> Build<TKey>(BaseDataRecordList<TKey> recordList)
    {
        return recordList.Source.GetRecordListOrdersNested()
            .Where(c => !string.IsNullOrEmpty(c.Name))
            .Select(c => new DataRecordListOrderParameters<TKey>(recordList, c))
            .Select(p => new Renderer<DataRecordListOrderParameters<TKey>, DataRecordListOrderLabel<TKey>>(p, Priority));
    }
}