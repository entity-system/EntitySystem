using System;
using System.Linq.Expressions;
using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Join.Factory;

public interface IDataJoinFactory : IRegistrable
{
    DataJoin<TParent, TChild, TMiddle, TParentService, TChildService> Build<TParent, TChild, TMiddle, TParentService, TChildService>(DataSource<TParent, TParentService> parentSource, DataSource<TChild, TChildService> childSource, Expression<Func<TParent, TMiddle>> parentMiddleSelector, Expression<Func<TChild, TMiddle>> childMiddleSelector)
        where TParent : IEntity, new()
        where TChild : IEntity, new()
        where TMiddle : IEntity
        where TParentService : IEntityService<TParent>
        where TChildService : IEntityService<TChild>;
}