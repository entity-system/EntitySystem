using EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Pair;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Join;

public interface IDataJoinReverseCloneJoinFeature<TChild, TChildService> : IDataPairReverseCloneJoinFeature<TChild, TChildService>
    where TChild : IEntity, new()
    where TChildService : IEntityService<TChild>
{
}