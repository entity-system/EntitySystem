using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Decimal.Property;

namespace EntitySystem.Client.Components.Data.Input.Decimal.Factory;

public class DataInputDecimalFactory : IDataInputDecimalFactory
{
    public IRenderer Build<TEntity>(BaseDataInput<IDataInputDecimalProperty<TEntity>, TEntity, decimal> input)
    {
        return new Renderer<BaseDataInput<IDataInputDecimalProperty<TEntity>, TEntity, decimal>, DataInputDecimal<TEntity>>(input);
    }
}