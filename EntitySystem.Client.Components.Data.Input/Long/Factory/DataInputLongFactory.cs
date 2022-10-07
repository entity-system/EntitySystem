using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Long.Property;

namespace EntitySystem.Client.Components.Data.Input.Long.Factory;

public class DataInputLongFactory : IDataInputLongFactory
{
    public IRenderer Build<TEntity>(BaseDataInput<IDataInputLongProperty<TEntity>, TEntity, long> input)
    {
        return new Renderer<BaseDataInput<IDataInputLongProperty<TEntity>, TEntity, long>, DataInputLong<TEntity>>(input);
    }
}