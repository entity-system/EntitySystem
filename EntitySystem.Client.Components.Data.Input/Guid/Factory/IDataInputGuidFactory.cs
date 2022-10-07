using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Guid.Property;

namespace EntitySystem.Client.Components.Data.Input.Guid.Factory;

public interface IDataInputGuidFactory : IRegistrable
{
    IRenderer Build<TEntity>(BaseDataInput<IDataInputGuidProperty<TEntity>, TEntity, System.Guid> input);
}