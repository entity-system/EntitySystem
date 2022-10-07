using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Components.Data.Output.Parameters;

namespace EntitySystem.Client.Components.Data.Output.Feature;

public interface IDataOutputFeatureProcessor : IRegistrable
{
    void Process<TKey, TEntity, TValue>(DataOutputParameters<TKey, TEntity, TValue> parameters);
}