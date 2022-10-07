using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Output.Feature;

public interface IDataOutputFormatter : IFeature, IRegistrable
{
    IEnumerable<IRenderer> Build<TKey, TEntity, TValue>(BaseDataOutput<TKey, TEntity, TValue> output);
}