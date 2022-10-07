using EntitySystem.Shared.Domain;

namespace EntitySystem.Server.Services;

public interface IUniqueService<TEntity> : ISessionService
    where TEntity : IEntity, IUnique
{
}