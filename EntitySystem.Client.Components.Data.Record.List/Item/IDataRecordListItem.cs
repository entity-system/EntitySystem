using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Components.Data.Record.List.Value;

namespace EntitySystem.Client.Components.Data.Record.List.Item;

public interface IDataRecordListItem<out TKey> : IFeatured
{
    TKey Key { get; }

    IEnumerable<IDataRecordListValue> GetRecordListValues();
}