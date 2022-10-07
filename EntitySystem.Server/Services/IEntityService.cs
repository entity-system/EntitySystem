using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntitySystem.Shared.Domain;
using EntitySystem.Shared.Models;
using EntitySystem.Shared.Query;

namespace EntitySystem.Server.Services;

public interface IEntityService<TEntity> : ISessionService
    where TEntity : class, IEntity
{
    public TService GetService<TService>();
    public QueryService GetQueryService();
    public Task SaveOrUpdateAsync(TEntity entity, bool force = false);
    public Task<Guid> QueryAsync();
    public Task<Guid> JoinAsync(Guid parentGuid, QueryJoin queryJoin);
    public Task<PageList<TEntity>> ListAsync(Guid guid, Query query);
    public Task DeleteAsync(TEntity entity, bool force = false);
    public Task DeleteAsync(List<TEntity> list, bool force = false);
    public Task DeleteAsync<TIntermediate, TValue>(Expression<Func<TEntity, TIntermediate>> intermediate, Expression<Func<TIntermediate, TValue>> selector, bool force = false, params TValue[] values);
    public Task DeleteAsync<TValue>(Expression<Func<TEntity, TValue>> selector, bool force = false, params TValue[] values);
    public Task DeleteAsync<TValue>(Expression<Func<TEntity, TValue>> selector, IQueryable<TValue> values, bool force = false);
    public Task DeleteByIdsAsync(bool force = false, params long[] ids);
    public IQueryable<TEntity> Query<TValue>(Expression<Func<TEntity, TValue>> selector, params TValue[] values);
    public IQueryable<TEntity> Query<TValue>(Expression<Func<TEntity, TValue>> selector, IQueryable<TValue> values);
    Task<TEntity> GetByIdAsync(long id, bool force = false);
}