using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Services;

public interface IDataService
{
    DataSource<TEntity, TService> Get<TEntity, TService>(string type, out DataSource<TEntity, TService> source, int targetDeep = 0, int joinDeep = 0)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>;

    DataSource<TEntity, TService> Get<TEntity, TService>(string type, int targetDeep = 0, int joinDeep = 0)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>;
}