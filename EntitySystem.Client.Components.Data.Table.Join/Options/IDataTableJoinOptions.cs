using EntitySystem.Client.Components.Data.Table.Join.Record;

namespace EntitySystem.Client.Components.Data.Table.Join.Options;

public interface IDataTableJoinOptions<out TParent>
{
    public IDataTableJoinRecord<TParent> GetJoinRecord();
}