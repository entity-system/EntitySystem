using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EntitySystem.Client.Components.Data.Header.Source;

public interface IDataHeaderSource<TEntity>
{
    Expression<Func<TEntity, bool>> GetIdEquation(TEntity entity);

    void AddCondition(Expression<Func<TEntity, bool>> expression, string name);

    void AddOrder<TValue>(Expression<Func<TEntity, TValue>> expression, bool descending, string name);

    Task<List<TEntity>> GetFilterHintsAsync(Expression<Func<TEntity, bool>> expression = null);
}