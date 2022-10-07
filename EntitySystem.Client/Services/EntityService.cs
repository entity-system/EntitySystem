using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EntitySystem.Client.Domain.Data.Condition;
using EntitySystem.Client.Domain.Data.Order;
using EntitySystem.Shared.Domain;
using EntitySystem.Shared.Query;
using Microsoft.Extensions.DependencyInjection;

namespace EntitySystem.Client.Services;

public class EntityService<TEntity> : IEntityService<TEntity> where TEntity : IEntity
{
    private readonly ILoadingService _loadingService;
    private readonly HttpClient _httpClient;
    private readonly IExceptionService _exceptionService;

    public string Uri { get; }

    public EntityService(string uri, IServiceProvider serviceProvider)
    {
        _loadingService = serviceProvider.GetService<ILoadingService>();
        _httpClient = serviceProvider.GetService<HttpClient>();
        _exceptionService = serviceProvider.GetService<IExceptionService>();

        Uri = uri;
    }

    public virtual async Task<TEntity> PutAsync(TEntity entity)
    {
        var content = JsonContent.Create(entity);

        var request = new HttpRequestMessage(HttpMethod.Put, Uri) { Content = content };

        var response = await LoadAsync(nameof(PutAsync), request);

        entity = await response.Content.ReadFromJsonAsync<TEntity>();

        return entity;
    }

    public virtual async Task<TEntity> GetByIdAsync(long id)
    {
        var guid = await QueryAsync();

        var query = new Query { Conditions = new List<QueryCondition> { DataCondition<TEntity>.Query(guid, e => e.Id == id) } };

        var list = await ListAsync(guid, query);

        return list.Page.SingleOrDefault();
    }

    public virtual async Task<Guid> QueryAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{Uri}/query");

        var response = await LoadAsync(nameof(QueryAsync), request);

        var appliers = await response.Content.ReadFromJsonAsync<Guid>();

        return appliers;
    }

    public virtual async Task<Guid> JoinAsync(Guid guid, QueryJoin join)
    {
        var content = JsonContent.Create(join);

        var request = new HttpRequestMessage(HttpMethod.Post, $"{Uri}/join/{guid}") { Content = content };

        var response = await LoadAsync(nameof(JoinAsync), request);

        var result = await response.Content.ReadFromJsonAsync<Guid>();

        return result;
    }

    public virtual async Task<TEntity> NewestOrDefaultAsync(Expression<Func<TEntity, bool>> expression = null)
    {
        var guid = await QueryAsync();

        var query = new Query();

        if (expression != null) query.Conditions = new List<QueryCondition> { DataCondition<TEntity>.Query(guid, expression) };

        query.Orders = new List<QueryOrder> { DataOrder<TEntity, long>.Query(guid, e => e.Id, true) };

        var list = await ListAsync(guid, query);

        return list.Page.FirstOrDefault();
    }

    public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression = null)
    {
        var list = await ListAsync(expression);

        return list.SingleOrDefault();
    }

    public virtual async Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression = null)
    {
        var guid = await QueryAsync();

        var query = new Query();

        if (expression != null) query.Conditions = new List<QueryCondition> { DataCondition<TEntity>.Query(guid, expression) };

        var list = await ListAsync(guid, query);

        return list.Page;
    }

    public virtual async Task<QueryList<TEntity>> ListAsync(Guid guid, Query query)
    {
        var content = JsonContent.Create(query);

        var request = new HttpRequestMessage(HttpMethod.Post, $"{Uri}/list/{guid}") { Content = content };

        var response = await LoadAsync(nameof(ListAsync), request);

        var list = await response.Content.ReadFromJsonAsync<QueryList<TEntity>>();

        return list;
    }

    public virtual async Task DeleteAsync(params TEntity[] entities)
    {
        var content = JsonContent.Create(entities.ToList());

        var request = new HttpRequestMessage(HttpMethod.Delete, Uri) { Content = content };

        await LoadAsync(nameof(DeleteAsync), request);
    }

    public async Task<HttpResponseMessage> LoadAsync(string action, HttpRequestMessage request)
    {
        return await _loadingService.LoadAsync(this, async () => await SendAsync(action, request));
    }

    protected virtual async Task<HttpResponseMessage> SendAsync(string action, HttpRequestMessage request)
    {
        return await RawSendAsync(request);
    }

    protected async Task<HttpResponseMessage> RawSendAsync(HttpRequestMessage request, params int[] validCodes)
    {
        var response = await _httpClient.SendAsync(request);

        await _exceptionService.ProcessAsync(request, response, validCodes);

        return response;
    }
}