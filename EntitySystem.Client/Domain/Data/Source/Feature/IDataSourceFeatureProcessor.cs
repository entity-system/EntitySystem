using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Source.Feature;

public interface IDataSourceFeatureProcessor : IRegistrable
{
    void Process<TEntity, TService>(DataSource<TEntity, TService> source)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>;
}