using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Client.Domain.Data.Source.Factory;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Services;

public class DataService : IDataService
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataService(IRegistrationProvider registrationProvider)
    {
        _registrationProvider = registrationProvider;
    }

    public DataSource<TEntity, TService> Get<TEntity, TService>(string type, out DataSource<TEntity, TService> source, int targetDeep = 0, int joinDeep = 0)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        return source = Get<TEntity, TService>(type, targetDeep, joinDeep);
    }

    public DataSource<TEntity, TService> Get<TEntity, TService>(string type, int targetDeep = 0, int joinDeep = 0)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        var factory = _registrationProvider.GetRegistration<IDataSourceFactory>();

        return factory.Build<TEntity, TService>(type, targetDeep, joinDeep);
    }
}