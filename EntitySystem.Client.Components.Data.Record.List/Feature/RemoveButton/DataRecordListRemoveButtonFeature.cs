using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.RemoveButton;

public class DataRecordListRemoveButtonFeature : IDataRecordListRemoveButtonFeature
{
    public const long Priority = 40;

    public IEnumerable<IRenderer> Build<TKey>(BaseDataRecordList<TKey> recordList)
    {
        if(!recordList.Selected.Any()) yield break;

        yield return new Renderer<BaseDataRecordList<TKey>, DataRecordListRemoveButton<TKey>>(recordList, Priority);
    }
}