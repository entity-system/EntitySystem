using System.Collections.Generic;
using System.Threading.Tasks;
using EntitySystem.Client.Components.Data.Record.List.Item;
using EntitySystem.Client.Components.Data.Record.List.Source;

namespace EntitySystem.Client.Components.Data.Record.List.Options;

public interface IDataRecordListOptions<TKey>
{
    IDataRecordListSource<TKey> GetRecordListSource();

    IEnumerable<IDataRecordListItem<TKey>> GetRecordListItems();

    Task OnAfterCreateEntityAsync(TKey key);

    Task OnRefreshAsync();
}