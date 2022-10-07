using EntitySystem.Server.Domain;
using EntitySystem.Server.Tree;

namespace EntitySystem.Server.Services;

public class QueryStoreService
{
    public GuidStore<INode> Nodes { get; set; } = new();
}