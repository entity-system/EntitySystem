using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.Pager;

public class DataRecordListPagerFeature : IDataRecordListPagerFeature
{
    public const long Priority = 70;

    public IEnumerable<IRenderer> Build<TKey>(BaseDataRecordList<TKey> recordList)
    {
        yield return new Renderer<BaseDataRecordList<TKey>, DataRecordListPager<TKey>>(recordList, Priority);
    }
}