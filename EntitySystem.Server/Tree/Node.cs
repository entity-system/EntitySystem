using System.Collections.Generic;

namespace EntitySystem.Server.Tree;

public class Node : INode
{
    public INode Parent { get; set; }

    public List<INode> Descendants { get; set; } = new();

    public void Assign(INode parent)
    {
        Parent = parent;

        Parent.Descendants.Add(this);
    }

    public INode GetRoot()
    {
        return Parent != null ? Parent.GetRoot() : this;
    }
}