using EntitySystem.Shared.Exceptions;

namespace EntitySystem.Server.Exceptions;

public class EntityBadGeneralFriendlyException<TEntity> : EntityBadFriendlyException<TEntity>
{
    public EntityBadGeneralFriendlyException(string action, string reason) : base(action, GeneralFriendlyException.GeneralMessage, reason)
    {
    }
}