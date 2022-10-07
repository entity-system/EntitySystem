using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Input.Target.Feature.NotFound;

public class DataInputTargetNotFoundFeature : IDataInputTargetNotFoundFeature
{
    public IEnumerable<IRenderer> Build<TParent, TChild>(BaseDataInputTarget<TParent, TChild> search)
    {
        if (search.List == null || search.List.Any()) yield break;

        yield return new Renderer<BaseDataInputTarget<TParent, TChild>, DataInputTargetNotFound<TParent, TChild>>(search);
    }
}