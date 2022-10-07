using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Header.Parameters;
using EntitySystem.Client.Components.Data.Header.Text.Property;

namespace EntitySystem.Client.Components.Data.Header.Text.Factory;

public interface IDataHeaderTextFactory : IRegistrable
{
    IRenderer Build<TEntity>(DataHeaderParameters<IDataHeaderTextProperty<TEntity>, TEntity, string> parameters);
}