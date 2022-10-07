using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Long.Property;

namespace EntitySystem.Client.Components.Data.Input.Long.Factory;

public interface IDataInputLongFactory : IRegistrable
{
    IRenderer Build<TEntity>(BaseDataInput<IDataInputLongProperty<TEntity>, TEntity, long> input);
}