using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Output.Feature.SimpleFormatter;

public class DataOutputSimpleFormatter : IDataOutputSimpleFormatter
{
    public IEnumerable<IRenderer> Build<TKey, TEntity, TValue>(BaseDataOutput<TKey, TEntity, TValue> output)
    {
        yield return new Renderer<BaseDataOutput<TKey, TEntity, TValue>, DataOutputSimple<TKey, TEntity, TValue>>(output);
    }
}