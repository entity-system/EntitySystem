using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Integer.Property;

namespace EntitySystem.Client.Components.Data.Input.Integer.Factory;

public class DataInputIntegerFactory : IDataInputIntegerFactory
{
    public IRenderer Build<TEntity>(BaseDataInput<IDataInputIntegerProperty<TEntity>, TEntity, int> input)
    {
        return new Renderer<BaseDataInput<IDataInputIntegerProperty<TEntity>, TEntity, int>, DataInputInteger<TEntity>>(input);
    }
}