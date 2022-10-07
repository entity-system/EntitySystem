using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Record.List.Options;
using EntitySystem.Client.Components.Data.Record.List.Source;

namespace EntitySystem.Client.Components.Data.Table.Join.Source;

public interface IDataTableJoinSource<TEntity> : IDataRecordListSource<TEntity>
{
    IRenderer BuildRecordList<TKey>(IDataRecordListOptions<TKey> options);
}