using System;
using EntitySystem.Client.Abstract.Domain.Feature;

namespace EntitySystem.Client.Components.Data.Output.Feature.LinkFormatter;

public class DataOutputLinkOptions<TKey> : IFeature
{
    public Func<TKey, string> LinkFactory { get; }

    public string LinkTarget { get; }

    public bool IsSelf => LinkTarget == "_self";

    public DataOutputLinkOptions(Func<TKey, string> linkFactory, string linkTarget)
    {
        LinkFactory = linkFactory;
        LinkTarget = linkTarget;
    }
}