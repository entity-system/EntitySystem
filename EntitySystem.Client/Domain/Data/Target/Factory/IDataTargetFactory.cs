using System;
using System.Linq.Expressions;
using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Target.Factory;

public interface IDataTargetFactory : IRegistrable
{
    DataTarget<TParent, TParentService, TChild, TChildService> Build<TParent, TParentService, TChild, TChildService>(DataSource<TParent, TParentService> parentSource, DataSource<TChild, TChildService> childSource, Expression<Func<TParent, TChild>> expression, Expression<Func<TChild, string>> childNameExpression)
        where TParent : IEntity, new()
        where TParentService : IEntityService<TParent>
        where TChild : IEntity, new()
        where TChildService : IEntityService<TChild>;
}