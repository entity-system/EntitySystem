using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Parameters;
using EntitySystem.Client.Components.Data.Input.Property;

namespace EntitySystem.Client.Components.Data.Input.Factory;

public interface IDataInputFactory : IRegistrable
{
    IRenderer Build<TProperty, TEntity, TValue>(DataInputParameters<TProperty, TEntity, TValue> parameters)
        where TProperty : IDataInputProperty<TEntity, TValue>;
}