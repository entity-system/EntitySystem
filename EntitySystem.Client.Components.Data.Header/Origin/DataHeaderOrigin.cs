using System;
using System.Linq.Expressions;
using EntitySystem.Client.Components.Data.Header.Property;
using EntitySystem.Client.Components.Data.Header.Source;
using EntitySystem.Shared.Abstract.Extensions;

namespace EntitySystem.Client.Components.Data.Header.Origin;

public class DataHeaderOrigin<TOrigin, TEntity, TValue> : IDataHeaderOrigin<TEntity>
{
    public IDataHeaderProperty<TEntity, TValue> Property { get; }

    public IDataHeaderSource<TOrigin> OriginSource { get; }

    public IDataHeaderSource<TEntity> EntitySource { get; }

    public Expression<Func<TOrigin, TEntity>> Expression { get; }

    public DataHeaderOrigin(IDataHeaderProperty<TEntity, TValue> property, IDataHeaderSource<TOrigin> originSource, IDataHeaderSource<TEntity> entitySource, Expression<Func<TOrigin, TEntity>> expression)
    {
        Property = property;
        OriginSource = originSource;
        EntitySource = entitySource;
        Expression = expression;
    }

    public void Restrict(TEntity entity)
    {
        OriginSource.AddCondition(Expression.Compose(EntitySource.GetIdEquation(entity)), Property.ToString(entity));
    }

    public void Order(bool descending)
    {
        OriginSource.AddOrder(Expression.Compose(Property.Expression), descending, Property.Name);
    }
}