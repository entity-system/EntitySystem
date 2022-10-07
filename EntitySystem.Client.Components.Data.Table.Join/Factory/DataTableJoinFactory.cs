using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Table.Join.Parameters;

namespace EntitySystem.Client.Components.Data.Table.Join.Factory;

public class DataTableJoinFactory : IDataTableJoinFactory
{
    public IRenderer Build<TParent, TChild>(DataTableJoinParameters<TParent, TChild> parameters)
    {
        return new Renderer<DataTableJoinParameters<TParent, TChild>, DataTableJoin<TParent, TChild>>(parameters);
    }
}