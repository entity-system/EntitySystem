using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Components.Data.Input.Parameters;
using EntitySystem.Client.Components.Data.Input.Property;

namespace EntitySystem.Client.Components.Data.Input.Feature;

public interface IDataInputFeatureFactory : IRegistrable
{
    IFeature Build<TProperty, TEntity, TValue>(IDataInputParameters<TProperty, TEntity, TValue> parameters)
        where TProperty : IDataInputProperty<TEntity, TValue>;
}