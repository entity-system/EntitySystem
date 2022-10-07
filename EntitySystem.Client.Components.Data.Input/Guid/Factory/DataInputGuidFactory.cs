using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Guid.Property;

namespace EntitySystem.Client.Components.Data.Input.Guid.Factory;

public class DataInputGuidFactory : IDataInputGuidFactory
{
    public IRenderer Build<TEntity>(BaseDataInput<IDataInputGuidProperty<TEntity>, TEntity, System.Guid> input)
    {
        return new Renderer<BaseDataInput<IDataInputGuidProperty<TEntity>, TEntity, System.Guid>, DataInputGuid<TEntity>>(input);
    }
}