using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Record.List.Item;

namespace EntitySystem.Client.Components.Data.Record.List.Feature;

public interface IDataRecordListColumnFeature : IDataRecordListItemFeature
{
    IEnumerable<IRenderer> BuildHeader<TKey>(BaseDataRecordList<TKey> recordList);

    IEnumerable<IRenderer> BuildOutput<TKey>(BaseDataRecordList<TKey> recordList, IDataRecordListItem<TKey> record, int index);
}