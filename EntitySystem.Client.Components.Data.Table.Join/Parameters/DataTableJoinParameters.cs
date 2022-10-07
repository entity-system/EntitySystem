using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Parameters;
using EntitySystem.Client.Components.Data.Table.Join.Options;

namespace EntitySystem.Client.Components.Data.Table.Join.Parameters;

public class DataTableJoinParameters<TParent, TChild> : Featured, IParameters
{
    public IDataTableJoin<TParent, TChild> Join { get; }

    public IDataTableJoinOptions<TParent> Options { get; }

    public DataTableJoinParameters(IDataTableJoin<TParent, TChild> join, IDataTableJoinOptions<TParent> options)
    {
        Join = join;
        Options = options;
    }
}