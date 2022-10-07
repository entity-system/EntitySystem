namespace EntitySystem.Server.Exceptions;

public class EntityBadFriendlyException<T> : EntityActionFriendlyException<T>, IEntityBadFriendlyException
{
    public EntityBadFriendlyException(string action, string notification, string reason) : base(action, notification, reason)
    {
    }
}