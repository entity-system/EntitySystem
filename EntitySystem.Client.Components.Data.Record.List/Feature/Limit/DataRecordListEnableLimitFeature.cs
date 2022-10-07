using EntitySystem.Client.Abstract.Domain.Feature;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.Limit;

public class DataRecordListEnableLimitFeature : IFeature
{
    public int Limit { get; set; }

    public DataRecordListEnableLimitFeature(int limit)
    {
        Limit = limit;
    }
}