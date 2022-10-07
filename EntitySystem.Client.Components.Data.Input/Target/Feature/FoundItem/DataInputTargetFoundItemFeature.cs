using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Input.Target.Feature.FoundItem;

internal class DataInputTargetFoundItemFeature : IDataInputTargetFoundItemFeature
{
    public IEnumerable<IRenderer> Build<TParent, TChild>(BaseDataInputTarget<TParent, TChild> target)
    {
        return (target.List ?? Enumerable.Empty<TChild>())
            .Select(entity => new DataInputTargetFoundItemParameters<TParent, TChild>(target, entity))
            .Select(parameters => new Renderer<DataInputTargetFoundItemParameters<TParent, TChild>, DataInputTargetFoundItem<TParent, TChild>>(parameters, 10));
    }
}