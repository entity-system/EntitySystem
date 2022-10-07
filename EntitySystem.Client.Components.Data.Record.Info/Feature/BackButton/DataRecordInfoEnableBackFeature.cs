using System;
using EntitySystem.Client.Abstract.Domain.Feature;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature.BackButton;

public class DataRecordInfoEnableBackFeature<TKey> : IFeature
{
    public DataRecordInfoBackButtonOptions<TKey> Options { get; }

    public DataRecordInfoEnableBackFeature(Func<TKey, string> linkFactory)
    {
        Options = new DataRecordInfoBackButtonOptions<TKey>(linkFactory);
    }
}