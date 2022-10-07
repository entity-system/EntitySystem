using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.RefreshButton;

public class DataRecordListRefreshButtonFeature : IDataRecordListRefreshButtonFeature
{
    public const long Priority = 24;

    public IEnumerable<IRenderer> Build<TKey>(BaseDataRecordList<TKey> recordList)
    {
        if (recordList.Selected.Any()) yield break;

        yield return new Renderer<BaseDataRecordList<TKey>, DataRecordListRefreshButton<TKey>>(recordList, Priority);
    }
}