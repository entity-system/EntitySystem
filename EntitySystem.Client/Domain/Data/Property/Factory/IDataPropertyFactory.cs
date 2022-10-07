using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Property.Factory;

public interface IDataPropertyFactory : IRegistrable
{
    void Process<TEntity, TValue>(DataProperty<TEntity, TValue> property)
        where TEntity : IEntity;
}