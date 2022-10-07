using System;
using System.Linq.Expressions;
using EntitySystem.Client.Domain.Data.Condition;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Source.Extensions.Condition;

public static class DataSourceConditionExtensions
{
    public static DataSource<TEntity, TService> Where<TEntity, TService>(this DataSource<TEntity, TService> dataSource, Expression<Func<TEntity, bool>> equals)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        var condition = new DataCondition<TEntity>(equals);

        condition.Assign(dataSource);

        return dataSource;
    }

    public static DataSource<TEntity, TService> Like<TEntity, TService>(this DataSource<TEntity, TService> dataSource, Expression<Func<TEntity, bool>> equals)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        var condition = new DataCondition<TEntity>(equals) { Like = true };

        condition.Assign(dataSource);

        return dataSource;
    }
}