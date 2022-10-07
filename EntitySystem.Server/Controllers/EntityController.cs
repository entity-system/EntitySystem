using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EntitySystem.Server.Attributes;
using EntitySystem.Server.Exceptions;
using EntitySystem.Server.Services;
using EntitySystem.Shared.Domain;
using EntitySystem.Shared.Exceptions;
using EntitySystem.Shared.Models;
using EntitySystem.Shared.Query;
using Microsoft.AspNetCore.Mvc;

namespace EntitySystem.Server.Controllers;

[ApiController]
public abstract class EntityController<TService, TEntity> : ControllerBase
    where TService : IEntityService<TEntity>
    where TEntity : class, IEntity
{
    protected TService Service { get; set; }

    protected EntityController(TService entityService)
    {
        Service = entityService;
    }

    [HttpPut]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Transaction]
    public virtual async Task<IActionResult> PutAsync([FromBody] TEntity entity)
    {
        return await TryAsync(async () =>
        {
            await Service.SaveOrUpdateAsync(entity);

            return new ObjectResult(entity);
        });
    }

    [HttpPost("query")]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
    [Produces("application/json")]
    [Session]
    public virtual async Task<IActionResult> QueryAsync()
    {
        return await TryAsync(async () =>
        {
            var result = await Service.QueryAsync();

            return new ObjectResult(result);
        });
    }

    [HttpPost("join/{guid:Guid}")]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Session]
    public virtual async Task<IActionResult> JoinAsync(Guid guid, [FromBody] QueryJoin queryJoin)
    {
        return await TryAsync(async () =>
        {
            var result = await Service.JoinAsync(guid, queryJoin);

            return new ObjectResult(result);
        });
    }

    [HttpPost("list/{guid:Guid}")]
    [Produces("application/json")]
    [Session]
    public virtual async Task<IActionResult> ListAsync(Guid guid, [FromBody] Query query)
    {
        return await TryAsync(async () =>
        {
            var list = await Service.ListAsync(guid, query);

            return new ObjectResult(list);
        });
    }

    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Transaction]
    public virtual async Task<IActionResult> DeleteAsync([FromBody] List<TEntity> list)
    {
        return await TryAsync(async () =>
        {
            await Service.DeleteAsync(list);

            return Ok();
        });
    }

    protected async Task<IActionResult> TryAsync(Func<Task<IActionResult>> action)
    {
        return await TryAsync(action());
    }

    protected virtual async Task<IActionResult> TryAsync(Task<IActionResult> task)
    {
        return await ExceptionHandler.TryAsync(task);
    }
}