using System;
using EntitySystem.Client.Abstract.Domain.Feature;

namespace EntitySystem.Client.Components.Data.Output.Feature.LinkFormatter;

public class DataOutputEnableLink<TKey> : IFeature
{
    public DataOutputLinkOptions<TKey> Options { get; }

    public DataOutputEnableLink(Func<TKey, string> linkFactory, string linkTarget)
    {
        Options = new DataOutputLinkOptions<TKey>(linkFactory, linkTarget);
    }
}