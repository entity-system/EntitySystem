using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Integer.Property;

namespace EntitySystem.Client.Components.Data.Input.Integer.Factory;

public interface IDataInputIntegerFactory : IRegistrable
{
    IRenderer Build<TEntity>(BaseDataInput<IDataInputIntegerProperty<TEntity>, TEntity, int> input);
}