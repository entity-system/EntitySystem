using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Input.Target.Feature;

public interface IDataInputTargetItemFeature : IRegistrable, IFeature
{
    IEnumerable<IRenderer> Build<TParent, TChild>(BaseDataInputTarget<TParent, TChild> target);
}