using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Domain.Data.Pair;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Pair;

public interface IDataPairReverseCloneJoinFeature<TChild, TChildService> : IFeature
    where TChild : IEntity, new()
    where TChildService : IEntityService<TChild>
{
    DataSource<TReversed, TReversedService> ReverseCloneJoin<TReversed, TReversedService>(DataSource<TChild, TChildService> childSource, Stack<IDataPair> path)
        where TReversed : IEntity, new()
        where TReversedService : IEntityService<TReversed>;
}