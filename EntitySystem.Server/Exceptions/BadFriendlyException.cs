using EntitySystem.Shared.Exceptions;

namespace EntitySystem.Server.Exceptions;

public class BadFriendlyException : FriendlyException, IEntityBadFriendlyException
{
    public BadFriendlyException(string notification, string message) : base(notification, message)
    {
    }
}