using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Record.List.Feature.Limit;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.AddButton;

public class DataRecordListAddButtonFeature : IDataRecordListAddButtonFeature
{
    public const long Priority = 20;

    public IEnumerable<IRenderer> Build<TKey>(BaseDataRecordList<TKey> recordList)
    {
        if (recordList.Selected.Any()) yield break;

        if (recordList.Source.Features.GetFeature<DataRecordListEnableLimitFeature>() is { } limitFeature && recordList.Source.MasterCount >= limitFeature.Limit) yield break;

        yield return new Renderer<BaseDataRecordList<TKey>, DataRecordListAddButton<TKey>>(recordList, Priority);
    }
}