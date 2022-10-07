using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.Title;

public class DataRecordListTitleFeature : IDataRecordListTitleFeature
{
    public IEnumerable<IRenderer> Build<TKey>(BaseDataRecordList<TKey> recordList)
    {
        yield return new Renderer<BaseDataRecordList<TKey>, DataRecordListTitle<TKey>>(recordList);
    }
}