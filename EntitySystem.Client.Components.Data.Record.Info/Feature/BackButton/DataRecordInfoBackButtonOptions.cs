using System;
using EntitySystem.Client.Abstract.Domain.Feature;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature.BackButton;

public class DataRecordInfoBackButtonOptions<TKey> : IFeature
{
    public Func<TKey, string> LinkFactory { get; }

    public DataRecordInfoBackButtonOptions(Func<TKey, string> linkFactory)
    {
        LinkFactory = linkFactory;
    }
}