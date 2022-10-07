using System;
using System.Threading.Tasks;
using EntitySystem.Server.Exceptions;
using EntitySystem.Server.Services;
using EntitySystem.Shared.Domain;
using EntitySystem.Shared.Exceptions;
using NHibernate.Linq;

namespace EntitySystem.Server.Extensions;

public static class UniqueServiceExtensions
{
    public static async Task<TEntity> GetByGuidAsync<TEntity>(this IUniqueService<TEntity> service, Guid guid)
        where TEntity : IEntity, IUnique
    {
        var entity = await service.GetSession().Query<TEntity>().SingleOrDefaultAsync(i => i.Guid == guid);

        if (entity == null) throw new EntityNotFoundGeneralFriendlyException<TEntity>(nameof(GetByGuidAsync), $"entity with guid ({guid})");

        return entity;
    }
}