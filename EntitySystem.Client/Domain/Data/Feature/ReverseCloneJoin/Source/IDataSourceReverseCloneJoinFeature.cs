using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Domain.Data.Pair;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Source;

public interface IDataSourceReverseCloneJoinFeature : IFeature
{
    public DataSource<TReverseEntity, TReverseService> ReverseCloneJoin<TReverseEntity, TReverseService>(Stack<IDataPair> path)
        where TReverseEntity : IEntity, new()
        where TReverseService : IEntityService<TReverseEntity>;

    IEnumerable<IEnumerable<IDataPair>> FindPathsBySource(IDataSource source, List<IDataPair> path = null);
}