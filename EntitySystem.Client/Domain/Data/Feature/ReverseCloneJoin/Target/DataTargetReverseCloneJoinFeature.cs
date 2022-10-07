using System.Collections.Generic;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Source;
using EntitySystem.Client.Domain.Data.Join.Factory;
using EntitySystem.Client.Domain.Data.Pair;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Client.Domain.Data.Target;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Target;

public class DataTargetReverseCloneJoinFeature<TParent, TParentService, TChild, TChildService> : IDataTargetReverseCloneJoinFeature<TChild, TChildService>
    where TParent : IEntity, new()
    where TParentService : IEntityService<TParent>
    where TChild : IEntity, new()
    where TChildService : IEntityService<TChild>
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataTarget<TParent, TParentService, TChild, TChildService> Target { get; }

    public DataTargetReverseCloneJoinFeature(IRegistrationProvider registrationProvider, DataTarget<TParent, TParentService, TChild, TChildService> target)
    {
        _registrationProvider = registrationProvider;
        Target = target;
    }

    public DataSource<TReverseEntity, TReverseService> ReverseCloneJoin<TReverseEntity, TReverseService>(DataSource<TChild, TChildService> childSource, Stack<IDataPair> path)
        where TReverseEntity : IEntity, new()
        where TReverseService : IEntityService<TReverseEntity>
    {
        var parentSource = Target.ParentSource.Clone();

        var factory = _registrationProvider.GetRegistration<IDataJoinFactory>();

        var clonedReverseJoin = factory.Build(childSource, parentSource, t => t, Target.Expression);

        childSource.Joins.Add(clonedReverseJoin);

        var feature = parentSource.Features.GetFeature<IDataSourceReverseCloneJoinFeature>();

        return feature.ReverseCloneJoin<TReverseEntity, TReverseService>(path);
    }
}