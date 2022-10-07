namespace EntitySystem.Shared.Exceptions;

public class GeneralFriendlyException : FriendlyException
{
    public const string GeneralMessage = "Something bad happened. Please contact support.";

    public GeneralFriendlyException(string message) : base(GeneralMessage, message)
    {
    }
}