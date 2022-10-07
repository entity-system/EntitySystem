using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Record.List.Item;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.SelectBox;

public class DataRecordListSelectBoxFeature : IDataRecordListSelectBoxFeature
{
    public IEnumerable<IRenderer> BuildHeader<TKey>(BaseDataRecordList<TKey> recordList)
    {
        yield return new Renderer<BaseDataRecordList<TKey>, DataRecordListSelectBoxHeader<TKey>>(recordList);
    }

    public IEnumerable<IRenderer> BuildOutput<TKey>(BaseDataRecordList<TKey> recordList, IDataRecordListItem<TKey> record, int index)
    {
        var parameters = new DataRecordListSelectBoxParameters<TKey>(recordList, record, index);

        yield return new Renderer<DataRecordListSelectBoxParameters<TKey>, DataRecordListSelectBox<TKey>>(parameters);
    }
}