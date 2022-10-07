using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Input.Target.Feature.CreateItem;

public class DataInputTargetCreateItemFeature : IDataInputTargetCreateItemFeature
{
    public IEnumerable<IRenderer> Build<TParent, TChild>(BaseDataInputTarget<TParent, TChild> target)
    {
        if (target.List == null) yield break;

        yield return new Renderer<BaseDataInputTarget<TParent, TChild>, DataInputTargetCreateItem<TParent, TChild>>(target, 20);
    }
}