using System.Collections.Generic;
using System.Threading.Tasks;
using EntitySystem.Client.Components.Data.Table.Join.Record;
using EntitySystem.Client.Components.Data.Table.Join.Source;

namespace EntitySystem.Client.Components.Data.Table.Join;

public interface IDataTableJoin<in TParent, TChild>
{
    IDataTableJoinSource<TChild> GetTableJoinChildSource();

    Task RequestChildrenAsync(TParent parent);

    IEnumerable<IDataTableJoinRecord<TChild>> CreateTableJoinChildRecords();

    Task OnAfterCreateChildAsync(TParent parent, TChild child);
}