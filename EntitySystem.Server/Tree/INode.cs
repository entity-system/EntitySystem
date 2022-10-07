using System.Collections.Generic;

namespace EntitySystem.Server.Tree;

public interface INode
{
    INode Parent { get; set; }

    List<INode> Descendants { get; set; }

    INode GetRoot();
}