using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Target.Property;

namespace EntitySystem.Client.Components.Data.Input.Target.Factory;

public interface IDataInputTargetFactory : IRegistrable
{
    IRenderer Build<TParent, TChild>(BaseDataInput<IDataInputTargetProperty<TParent, TChild>, TParent, TChild> input);
}