using System;

namespace EntitySystem.Server.Tree;

public class TableNode : Node
{
    public Guid Guid { get; set; }

    public QueryJoiner Joiner { get; set; }
}