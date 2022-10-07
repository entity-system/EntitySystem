namespace EntitySystem.Shared.Models;

public class FriendlyExceptionModel
{
    public const string DefaultMessage = "Something bad happened. Please contact support.";

    public string Notification { get; set; }

    public string Message { get; set; }
}