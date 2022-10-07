using EntitySystem.Client.Components.Data.Record.List.Source;

namespace EntitySystem.Client.Components.Data.Record.List.Services;

public interface IDataRecordListNavigationService
{
    bool TryNavigateToRecordInfo<TKey>(IDataRecordListSource<TKey> source, TKey key);
}