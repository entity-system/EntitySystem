using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Join.Feature;

public interface IDataJoinFeatureProcessor : IRegistrable
{
    void Process<TParent, TChild, TMiddle, TParentService, TChildService>(DataJoin<TParent, TChild, TMiddle, TParentService, TChildService> join)
        where TParent : IEntity, new()
        where TChild : IEntity, new()
        where TMiddle : IEntity
        where TParentService : IEntityService<TParent>
        where TChildService : IEntityService<TChild>;
}