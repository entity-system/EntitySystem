using System.Collections.Generic;
using System.Threading.Tasks;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Record.Info.Options;
using EntitySystem.Client.Components.Data.Record.Info.Source;
using EntitySystem.Client.Components.Data.Record.List.Options;
using EntitySystem.Client.Components.Data.Record.List.Source;
using EntitySystem.Client.Components.Data.Table.Record;

namespace EntitySystem.Client.Components.Data.Table.Source;

public interface IDataTableSource<TEntity> :
    IDataRecordInfoSource<TEntity>,
    IDataRecordListSource<TEntity>
{
    Task RequestNestedAsync();

    Task RequestAsync();

    void AddCondition(long id);

    IEnumerable<IDataTableRecord<TEntity>> CreateTableRecordsNested();

    IEnumerable<IDataTableRecord<TEntity>> CreateTableRecords();

    IRenderer BuildRecordList<TKey>(IDataRecordListOptions<TKey> options);

    IRenderer BuildRecordInfo<TKey>(IDataRecordInfoOptions<TKey> options);
}