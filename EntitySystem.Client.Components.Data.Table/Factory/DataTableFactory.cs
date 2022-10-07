using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Table.Parameters;

namespace EntitySystem.Client.Components.Data.Table.Factory;

public class DataTableFactory : IDataTableFactory
{
    public IRenderer Build<TEntity>(DataTableParameters<TEntity> parameters)
    {
        return parameters.Build<DataTable<TEntity>>();
    }
}