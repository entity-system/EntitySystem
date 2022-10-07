using System;
using System.Linq.Expressions;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Components.Data.Header.Source;

namespace EntitySystem.Client.Components.Data.Header.Property;

public interface IDataHeaderProperty<TEntity, TValue> : IFeatured
{
    long Priority { get; }

    string Name { get; set; }

    Expression<Func<TEntity, TValue>> Expression { get; }

    Func<TEntity, TValue> Getter { get; }

    IDataHeaderSource<TEntity> GetDataHeaderSource();

    string ToString(TEntity entity);
}