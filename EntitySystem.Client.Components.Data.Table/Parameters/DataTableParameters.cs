using EntitySystem.Client.Abstract.Components;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Table.Options;
using EntitySystem.Client.Components.Data.Table.Source;

namespace EntitySystem.Client.Components.Data.Table.Parameters;

public class DataTableParameters<TEntity> : Featured, IDataTableParameters<TEntity>
{
    public IDataTableSource<TEntity> Source { get; set; }

    public IDataTableOptions Options { get; set; }

    public DataTableParameters(IDataTableSource<TEntity> source, IDataTableOptions options)
    {
        Source = source;
        Options = options;
    }

    public IRenderer Build<TComponent>() where TComponent : BaseRendered<IDataTableParameters<TEntity>>
    {
        return new Renderer<IDataTableParameters<TEntity>, TComponent>(this);
    }
}