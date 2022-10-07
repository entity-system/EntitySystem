using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Header.Parameters;
using EntitySystem.Client.Components.Data.Header.Property;

namespace EntitySystem.Client.Components.Data.Header.Factory;

public interface IDataHeaderFactory : IRegistrable
{
    IRenderer Build<TProperty, TEntity, TValue>(DataHeaderParameters<TProperty, TEntity, TValue> parameters)
        where TProperty : IDataHeaderProperty<TEntity, TValue>;
}