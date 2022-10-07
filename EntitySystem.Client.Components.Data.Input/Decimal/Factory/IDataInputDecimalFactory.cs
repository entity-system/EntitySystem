using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Decimal.Property;

namespace EntitySystem.Client.Components.Data.Input.Decimal.Factory;

public interface IDataInputDecimalFactory : IRegistrable
{
    IRenderer Build<TEntity>(BaseDataInput<IDataInputDecimalProperty<TEntity>, TEntity, decimal> input);
}