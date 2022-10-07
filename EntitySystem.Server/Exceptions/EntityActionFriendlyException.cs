using EntitySystem.Shared.Exceptions;

namespace EntitySystem.Server.Exceptions;

public abstract class EntityActionFriendlyException<TEntity> : FriendlyException
{
    protected EntityActionFriendlyException(string action, string notification, string reason) : base(notification, CreateActionMessage(action, reason))
    {
    }

    public const string ActionMessage = "Cannot do ({0}) for ({1}), because {2}.";

    public static string CreateActionMessage(string action, string reason)
    {
        return string.Format(ActionMessage, action, typeof(TEntity).Name, reason);
    }
}