using System.Collections.Generic;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Source;
using EntitySystem.Client.Domain.Data.Join;
using EntitySystem.Client.Domain.Data.Join.Factory;
using EntitySystem.Client.Domain.Data.Pair;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Join;

public class DataJoinReverseCloneJoinFeature<TParent, TChild, TMiddle, TParentService, TChildService> : IDataJoinReverseCloneJoinFeature<TChild, TChildService>
    where TParent : IEntity, new()
    where TChild : IEntity, new()
    where TMiddle : IEntity
    where TParentService : IEntityService<TParent>
    where TChildService : IEntityService<TChild>
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataJoin<TParent, TChild, TMiddle, TParentService, TChildService> Join { get; }

    public DataJoinReverseCloneJoinFeature(IRegistrationProvider registrationProvider, DataJoin<TParent, TChild, TMiddle, TParentService, TChildService> join)
    {
        _registrationProvider = registrationProvider;

        Join = join;
    }

    public DataSource<TReverseEntity, TReverseService> ReverseCloneJoin<TReverseEntity, TReverseService>(DataSource<TChild, TChildService> childSource, Stack<IDataPair> path)
        where TReverseEntity : IEntity, new()
        where TReverseService : IEntityService<TReverseEntity>
    {
        var parentSource = Join.ParentSource.Clone();

        var factory = _registrationProvider.GetRegistration<IDataJoinFactory>();

        var clonedReverseJoin = factory.Build(childSource, parentSource, Join.ChildMiddleExpression, Join.ParentMiddleExpression);

        childSource.Joins.Add(clonedReverseJoin);

        var feature = parentSource.Features.GetFeature<IDataSourceReverseCloneJoinFeature>();

        return feature.ReverseCloneJoin<TReverseEntity, TReverseService>(path);
    }
}