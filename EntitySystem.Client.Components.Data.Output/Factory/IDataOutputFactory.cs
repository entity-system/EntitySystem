using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Output.Parameters;

namespace EntitySystem.Client.Components.Data.Output.Factory;

public interface IDataOutputFactory : IRegistrable
{
    IRenderer Build<TKey, TEntity, TValue>(DataOutputParameters<TKey, TEntity, TValue> parameters);
}