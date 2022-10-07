using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Target.Feature;

public interface IDataTargetFeatureProcessor : IRegistrable
{
    void Process<TParent, TParentService, TChild, TChildService>(DataTarget<TParent, TParentService, TChild, TChildService> target)
        where TParent : IEntity, new()
        where TParentService : IEntityService<TParent>
        where TChild : IEntity, new()
        where TChildService : IEntityService<TChild>;
}