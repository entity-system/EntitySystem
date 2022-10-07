using EntitySystem.Client.Components.Data.Record.List.Parameters;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.RefreshButton;

public class DataRecordListRefreshButtonProcessor : IDataRecordListRefreshButtonProcessor
{
    private readonly IDataRecordListRefreshButtonFeature _refreshButtonFeature;

    public DataRecordListRefreshButtonProcessor(IDataRecordListRefreshButtonFeature refreshButtonFeature)
    {
        _refreshButtonFeature = refreshButtonFeature;
    }

    public void Process<TEntity>(IDataRecordListParameters<TEntity> parameters)
    {
        parameters.Features.AddFeature(_refreshButtonFeature);
    }
}