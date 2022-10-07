using EntitySystem.Client.Abstract.Domain.Parameters;
using EntitySystem.Client.Components.Data.Table.Options;
using EntitySystem.Client.Components.Data.Table.Source;

namespace EntitySystem.Client.Components.Data.Table.Parameters;

public interface IDataTableParameters<TEntity> : IParameters
{
    IDataTableSource<TEntity> Source { get; set; }
    IDataTableOptions Options { get; set; }
}