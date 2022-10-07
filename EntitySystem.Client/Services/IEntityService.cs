using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EntitySystem.Shared.Domain;
using EntitySystem.Shared.Query;

namespace EntitySystem.Client.Services;

public interface IEntityService<TEntity> where TEntity : IEntity
{
    string Uri { get; }

    Task<TEntity> PutAsync(TEntity entity);

    Task<Guid> QueryAsync();

    Task<Guid> JoinAsync(Guid guid, QueryJoin join);

    Task<QueryList<TEntity>> ListAsync(Guid guid, Query query);

    Task DeleteAsync(params TEntity[] entities);

    Task<HttpResponseMessage> LoadAsync(string action, HttpRequestMessage request);
    Task<TEntity> GetByIdAsync(long id);
}