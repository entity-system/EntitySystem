using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Components.Data.Header.Parameters;
using EntitySystem.Client.Components.Data.Header.Property;

namespace EntitySystem.Client.Components.Data.Header.Feature;

public interface IDataHeaderFeatureProcessor : IRegistrable
{
    void Process<TProperty, TEntity, TValue>(IDataHeaderParameters<TProperty, TEntity, TValue> parameters)
        where TProperty : IDataHeaderProperty<TEntity, TValue>;
}