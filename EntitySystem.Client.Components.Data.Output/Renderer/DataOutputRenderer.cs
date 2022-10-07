using System.Linq;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Output.Parameters;

namespace EntitySystem.Client.Components.Data.Output.Renderer;

public class DataOutputRenderer<TKey, TEntity, TValue, TComponent> : Renderer<IDataOutputParameters<TKey, TEntity, TValue>, TComponent>
    where TComponent : BaseDataOutput<TKey, TEntity, TValue>
{
    public DataOutputRenderer(IDataOutputParameters<TKey, TEntity, TValue> parameters) : base(parameters, parameters.Value.GetOutputProperty().Priority, ToRawText(parameters))
    {
    }

    private static string ToRawText(IDataOutputParameters<TKey, TEntity, TValue> parameters)
    {
        return string.Join(", ", parameters.Value.GetOutputObject().Entities.Select(e => parameters.Value.GetOutputProperty().ToString(e)));
    }
}