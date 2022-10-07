using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Domain.Data.Join;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Join;

public class DataJoinReverseCloneJoinFeatureProcessor : IDataJoinReverseCloneJoinFeatureProcessor
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataJoinReverseCloneJoinFeatureProcessor(IRegistrationProvider registrationProvider)
    {
        _registrationProvider = registrationProvider;
    }

    public void Process<TParent, TChild, TMiddle, TParentService, TChildService>(DataJoin<TParent, TChild, TMiddle, TParentService, TChildService> join)
        where TParent : IEntity, new()
        where TChild : IEntity, new()
        where TMiddle : IEntity
        where TParentService : IEntityService<TParent>
        where TChildService : IEntityService<TChild>
    {
        var feature = new DataJoinReverseCloneJoinFeature<TParent, TChild, TMiddle, TParentService, TChildService>(_registrationProvider, join);

        join.Features.AddFeature(feature);
    }
}