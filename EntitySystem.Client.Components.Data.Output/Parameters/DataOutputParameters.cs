using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Output.Options;
using EntitySystem.Client.Components.Data.Output.Renderer;
using EntitySystem.Client.Components.Data.Output.Value;

namespace EntitySystem.Client.Components.Data.Output.Parameters;

public class DataOutputParameters<TKey, TEntity, TValue> : Featured, IDataOutputParameters<TKey, TEntity, TValue>
{
    public IDataOutputValue<TKey, TEntity, TValue> Value { get; }

    public IDataOutputOptions Options { get; }

    public DataOutputParameters(IDataOutputValue<TKey, TEntity, TValue> value, IDataOutputOptions options)
    {
        Value = value;
        Options = options;
    }

    public IRenderer Build<TComponent>() where TComponent : BaseDataOutput<TKey, TEntity, TValue>
    {
        return new DataOutputRenderer<TKey, TEntity, TValue, TComponent>(this);
    }
}