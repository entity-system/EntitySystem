using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Text.Property;

namespace EntitySystem.Client.Components.Data.Input.Text.Factory;

public class DataInputTextFactory : IDataInputTextFactory
{
    public IRenderer Build<TEntity>(BaseDataInput<IDataInputTextProperty<TEntity>, TEntity, string> input)
    {
        return new Renderer<BaseDataInput<IDataInputTextProperty<TEntity>, TEntity, string>, DataInputText<TEntity>>(input);
    }
}