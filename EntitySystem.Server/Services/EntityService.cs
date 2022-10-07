using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EntitySystem.Server.Exceptions;
using EntitySystem.Shared.Abstract.Extensions;
using EntitySystem.Shared.Domain;
using EntitySystem.Shared.Exceptions;
using EntitySystem.Shared.Extensions;
using EntitySystem.Shared.Models;
using EntitySystem.Shared.Query;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Linq;

namespace EntitySystem.Server.Services;

public abstract class EntityService<TEntity> : IEntityService<TEntity> where TEntity : class, IEntity
{
    private readonly DatabaseService _databaseService;
    private readonly QueryService _queryService;
    private readonly IServiceProvider _serviceProvider;

    protected EntityService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _databaseService = serviceProvider.GetService<DatabaseService>();
        _queryService = serviceProvider.GetService<QueryService>();
    }

    public ISession GetSession()
    {
        return _databaseService.GetSession();
    }

    public TService GetService<TService>()
    {
        return _serviceProvider.GetService<TService>();
    }

    public QueryService GetQueryService()
    {
        return _queryService;
    }

    protected virtual Task VerifyAsync(string action, TEntity entity, TEntity current = null)
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnBeforeSaveAsync(TEntity entity, TEntity current = null)
    {
        return Task.CompletedTask;
    }

    public virtual async Task SaveOrUpdateAsync(TEntity entity, bool force = false)
    {
        var isNew = entity.IsNew();

        if (!force) await VerifyAsync(nameof(SaveOrUpdateAsync), entity);

        await OnBeforeSaveAsync(entity);

        await GetSession().SaveOrUpdateAsync(entity);

        if (isNew) await OnAfterSaveNewAsync(entity);

        await OnAfterSaveAsync(entity);
    }

    protected virtual Task OnAfterSaveNewAsync(TEntity entity)
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnAfterSaveAsync(TEntity entity)
    {
        return Task.CompletedTask;
    }

    protected virtual Task CensorAsync(TEntity entity)
    {
        return Task.CompletedTask;
    }

    public virtual async Task<TEntity> GetByIdAsync(long id, bool force = false)
    {
        if (id == 0) throw new EntityNotFoundGeneralFriendlyException<TEntity>(nameof(GetByIdAsync), 0);

        var entity = await GetSession().Query<TEntity>().FirstOrDefaultAsync(i => i.Id == id);

        if (entity == null) throw new EntityNotFoundGeneralFriendlyException<TEntity>(nameof(GetByIdAsync), id);

        return entity;
    }

    protected virtual async IAsyncEnumerable<Expression<Func<TEntity, bool>>> GetRestrictionsAsync()
    {
        await Task.CompletedTask;

        yield break;
    }

    public virtual async Task<Guid> QueryAsync()
    {
        return GetQueryService().Query(await GetRestrictionsAsync().ToListAsync());
    }

    public virtual async Task<Guid> JoinAsync(Guid parentGuid, QueryJoin queryJoin)
    {
        return GetQueryService().Join(parentGuid, queryJoin, await GetRestrictionsAsync().ToListAsync());
    }

    public virtual async Task<PageList<TEntity>> ListAsync(Guid guid, Query query)
    {
        var list = await _queryService.List<TEntity>(guid, query);

        foreach (var member in list.Page) await CensorAsync(member);

        return list;
    }

    public async Task DeleteAsync(TEntity entity, bool force = false)
    {
        await DeleteByIdsAsync(force, entity.Id);
    }

    public async Task DeleteAsync(List<TEntity> list, bool force = false)
    {
        await DeleteByIdsAsync(force, list.Select(e => e.Id).ToArray());
    }

    public async Task DeleteByIdsAsync(bool force = false, params long[] ids)
    {
        await DeleteAsync(e => e.Id, force, ids);
    }

    public async Task DeleteAsync<TIntermediate, TValue>(Expression<Func<TEntity, TIntermediate>> intermediate, Expression<Func<TIntermediate, TValue>> selector, bool force = false, params TValue[] values)
    {
        await DeleteAsync(intermediate.Compose(selector), force, values);
    }

    public virtual async Task DeleteAsync<TValue>(Expression<Func<TEntity, TValue>> selector, bool force = false, params TValue[] values)
    {
        var query = Query(selector, values);

        if (!force) query = (await GetRestrictionsAsync().ToListAsync()).Aggregate(query, (c, n) => c.Where(n));

        await DeleteAsync(query);
    }

    public virtual async Task DeleteAsync<TValue>(Expression<Func<TEntity, TValue>> selector, IQueryable<TValue> values, bool force = false)
    {
        var query = Query(selector, values);

        if (!force) query = (await GetRestrictionsAsync().ToListAsync()).Aggregate(query, (c, n) => c.Where(n));

        await DeleteAsync(query);
    }

    protected async Task DeleteAsync(IQueryable<TEntity> queryable)
    {
        await Query().Where(e => queryable.Select(n => n.Id).Contains(e.Id)).DeleteAsync();
    }

    public IQueryable<TEntity> Query<TValue>(Expression<Func<TEntity, TValue>> selector, params TValue[] values)
    {
        return Query().Where(selector.Compose(i => values.Contains(i)));
    }

    public IQueryable<TEntity> Query<TValue>(Expression<Func<TEntity, TValue>> selector, List<TValue> values)
    {
        return Query().Where(selector.Compose(i => values.Contains(i)));
    }

    public IQueryable<TEntity> Query<TValue>(Expression<Func<TEntity, TValue>> selector, IQueryable<TValue> values)
    {
        return Query().Where(selector.Compose(i => values.Contains(i)));
    }

    public IQueryable<TEntity> Query()
    {
        return GetSession().Query<TEntity>();
    }

    protected Exception Bad(string action, string notification, string reason)
    {
        return new EntityBadFriendlyException<TEntity>(action, notification, reason );
    }

    protected Exception BadGeneral(string action, string reason)
    {
        return new EntityBadGeneralFriendlyException<TEntity>(action, reason);
    }

    protected Exception NotFound(string action, string notification, string reason)
    {
        return new EntityNotFoundFriendlyException<TEntity>(action, notification,reason);
    }

    protected Exception NotFoundGeneral(string action, string reason)
    {
        return new EntityNotFoundGeneralFriendlyException<TEntity>(action, reason);
    }
}