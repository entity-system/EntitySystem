using EntitySystem.Client.Abstract.Domain.Parameters;
using EntitySystem.Client.Components.Data.Output.Options;
using EntitySystem.Client.Components.Data.Output.Value;

namespace EntitySystem.Client.Components.Data.Output.Parameters;

public interface IDataOutputParameters<TKey, TEntity, out TValue> : IParameters
{
    IDataOutputValue<TKey, TEntity, TValue> Value { get; }

    IDataOutputOptions Options { get; }
}