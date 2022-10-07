using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Source;

public class DataSourceReverseCloneJoinFeatureProcessor : IDataSourceReverseCloneJoinFeatureProcessor
{
    public void Process<TEntity, TService>(DataSource<TEntity, TService> source)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        var feature = new DataSourceReverseCloneJoinFeature<TEntity, TService>(source);

        source.Features.AddFeature(feature);
    }
}