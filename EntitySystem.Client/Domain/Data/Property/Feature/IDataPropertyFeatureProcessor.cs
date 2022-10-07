using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Property.Feature;

public interface IDataPropertyFeatureProcessor : IRegistrable
{
    public void Process<TEntity, TValue>(DataProperty<TEntity, TValue> property)
        where TEntity : IEntity;
}