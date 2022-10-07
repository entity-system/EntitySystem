using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Domain.Data.Target;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Target;

public class DataTargetReverseCloneJoinFeatureProcessor : IDataTargetReverseCloneJoinFeatureProcessor
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataTargetReverseCloneJoinFeatureProcessor(IRegistrationProvider registrationProvider)
    {
        _registrationProvider = registrationProvider;
    }

    public void Process<TParent, TParentService, TChild, TChildService>(DataTarget<TParent, TParentService, TChild, TChildService> target)
        where TParent : IEntity, new()
        where TParentService : IEntityService<TParent>
        where TChild : IEntity, new()
        where TChildService : IEntityService<TChild>
    {
        var feature = new DataTargetReverseCloneJoinFeature<TParent, TParentService, TChild, TChildService>(_registrationProvider, target);

        target.Features.AddFeature(feature);
    }
}