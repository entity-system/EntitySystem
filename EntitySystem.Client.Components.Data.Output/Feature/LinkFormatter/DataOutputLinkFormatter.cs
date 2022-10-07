using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Output.Feature.LinkFormatter;

public class DataOutputLinkFormatter : IDataOutputLinkFormatter
{
    public IEnumerable<IRenderer> Build<TKey, TEntity, TValue>(BaseDataOutput<TKey, TEntity, TValue> output)
    {
        var parameters = new DataOutputLinkParameters<TKey, TEntity, TValue>(output);

        yield return new Renderer<DataOutputLinkParameters<TKey, TEntity, TValue>, DataOutputLink<TKey, TEntity, TValue>>(parameters, -10);
    }
}