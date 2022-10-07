using System.Threading.Tasks;

namespace EntitySystem.Client.Components.Data.Record.Info.Options;

public interface IDataRecordInfoOptions<TKey>
{
    IDataRecordInfo<TKey> GetRecordInfo();

    Task OnBeforeSaveOrUpdateAsync(TKey key);

    Task OnRefreshAsync();
}