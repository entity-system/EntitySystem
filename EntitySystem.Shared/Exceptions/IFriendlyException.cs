namespace EntitySystem.Shared.Exceptions;

public interface IFriendlyException
{
    string Notification { get;}

    string Message { get;}
}