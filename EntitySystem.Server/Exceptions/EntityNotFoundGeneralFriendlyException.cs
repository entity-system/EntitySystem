using EntitySystem.Shared.Exceptions;

namespace EntitySystem.Server.Exceptions;

public class EntityNotFoundGeneralFriendlyException<TEntity> : EntityNotFoundFriendlyException<TEntity>
{
    public EntityNotFoundGeneralFriendlyException(string action, string description) : base(action, GeneralFriendlyException.GeneralMessage, description)
    {
    }

    public EntityNotFoundGeneralFriendlyException(string action, long id) : base(action, GeneralFriendlyException.GeneralMessage, id)
    {
    }
}