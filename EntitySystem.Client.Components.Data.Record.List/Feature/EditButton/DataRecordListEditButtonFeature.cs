using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.EditButton;

public class DataRecordListEditButtonFeature : IDataRecordListEditButtonFeature
{
    public const long Priority = 30;

    public IEnumerable<IRenderer> Build<TKey>(BaseDataRecordList<TKey> recordList)
    {
        if (recordList.Selected.Count() != 1) yield break;

        yield return new Renderer<BaseDataRecordList<TKey>, DataRecordListEditButton<TKey>>(recordList, Priority);
    }
}