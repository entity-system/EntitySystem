namespace EntitySystem.Client.Abstract.Domain.Feature;

public class Featured : IFeatured
{
    public Features Features { get; } = new();
}