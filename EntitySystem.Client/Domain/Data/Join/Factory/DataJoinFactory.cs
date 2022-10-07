using System;
using System.Linq.Expressions;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Domain.Data.Join.Feature;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Join.Factory;

public class DataJoinFactory : IDataJoinFactory
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataJoinFactory(IRegistrationProvider registrationProvider)
    {
        _registrationProvider = registrationProvider;
    }

    public DataJoin<TParent, TChild, TMiddle, TParentService, TChildService> Build<TParent, TChild, TMiddle, TParentService, TChildService>(DataSource<TParent, TParentService> parentSource, DataSource<TChild, TChildService> childSource, Expression<Func<TParent, TMiddle>> parentMiddleSelector, Expression<Func<TChild, TMiddle>> childMiddleSelector)
        where TParent : IEntity, new()
        where TChild : IEntity, new()
        where TMiddle : IEntity
        where TParentService : IEntityService<TParent>
        where TChildService : IEntityService<TChild>
    {
        var join = new DataJoin<TParent, TChild, TMiddle, TParentService, TChildService>(parentSource, childSource, parentMiddleSelector, childMiddleSelector);

        foreach (var processor in _registrationProvider.GetRegistrationsOf<IDataJoinFeatureProcessor>())
        {
            processor.Process(join);
        }

        return join;
    }
}