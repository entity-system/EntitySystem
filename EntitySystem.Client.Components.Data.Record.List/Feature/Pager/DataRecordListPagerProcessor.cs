using EntitySystem.Client.Components.Data.Record.List.Parameters;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.Pager;

public class DataRecordListPagerProcessor : IDataRecordListPagerProcessor
{
    private readonly IDataRecordListPagerFeature _pagerFeature;

    public DataRecordListPagerProcessor(IDataRecordListPagerFeature pagerFeature)
    {
        _pagerFeature = pagerFeature;
    }

    public void Process<TEntity>(IDataRecordListParameters<TEntity> parameters)
    {
        parameters.Features.AddFeature(_pagerFeature);
    }
}