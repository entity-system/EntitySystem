using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Text.Property;

namespace EntitySystem.Client.Components.Data.Input.Text.Factory;

public interface IDataInputTextFactory : IRegistrable
{
    IRenderer Build<TEntity>(BaseDataInput<IDataInputTextProperty<TEntity>, TEntity, string> input);
}