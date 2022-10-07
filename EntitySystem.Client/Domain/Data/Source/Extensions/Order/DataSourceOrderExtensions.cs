using System;
using System.Linq.Expressions;
using EntitySystem.Client.Domain.Data.Order;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Source.Extensions.Order;

public static class DataSourceOrderExtensions
{
    public static DataSource<TEntity, TService> OrderBy<TEntity, TService, TValue>(this DataSource<TEntity, TService> dataSource, Expression<Func<TEntity, TValue>> selector)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        var order = new DataOrder<TEntity, TValue>(selector);

        order.Assign(dataSource);

        return dataSource;
    }

    public static DataSource<TEntity, TService> OrderByDescending<TEntity, TService, TValue>(this DataSource<TEntity, TService> dataSource, Expression<Func<TEntity, TValue>> selector)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        var order = new DataOrder<TEntity, TValue>(selector, true);

        order.Assign(dataSource);

        return dataSource;
    }
}