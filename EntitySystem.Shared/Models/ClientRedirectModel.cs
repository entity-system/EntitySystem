namespace EntitySystem.Shared.Models;

public class ClientRedirectModel
{
    public string Uri { get; set; }

    public string Message { get; set; }

    public bool ClientSide { get; set; }
}