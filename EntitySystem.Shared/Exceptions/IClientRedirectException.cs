namespace EntitySystem.Shared.Exceptions;

public interface IClientRedirectException
{
    string Uri { get; }

    string Message { get; }

    bool ClientSide { get; }
}