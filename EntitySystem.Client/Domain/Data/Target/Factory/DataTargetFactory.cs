using System;
using System.Linq.Expressions;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Client.Domain.Data.Target.Feature;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Target.Factory;

public class DataTargetFactory : IDataTargetFactory
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataTargetFactory(IRegistrationProvider registrationProvider)
    {
        _registrationProvider = registrationProvider;
    }

    public DataTarget<TParent, TParentService, TChild, TChildService> Build<TParent, TParentService, TChild, TChildService>(DataSource<TParent, TParentService> parentSource, DataSource<TChild, TChildService> childSource, Expression<Func<TParent, TChild>> expression, Expression<Func<TChild, string>> childNameExpression)
        where TParent : IEntity, new()
        where TParentService : IEntityService<TParent>
        where TChild : IEntity, new()
        where TChildService : IEntityService<TChild>
    {
        var target = new DataTarget<TParent, TParentService, TChild, TChildService>(parentSource, childSource, expression, childNameExpression);

        foreach (var processor in _registrationProvider.GetRegistrationsOf<IDataTargetFeatureProcessor>())
        {
            processor.Process(target);
        }

        return target;
    }
}