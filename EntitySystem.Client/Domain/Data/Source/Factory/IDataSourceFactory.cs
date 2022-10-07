using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Source.Factory;

public interface IDataSourceFactory : IRegistrable
{
    DataSource<TEntity, TService> Build<TEntity, TService>(string type, int targetDeep, int joinDeep)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>;
}