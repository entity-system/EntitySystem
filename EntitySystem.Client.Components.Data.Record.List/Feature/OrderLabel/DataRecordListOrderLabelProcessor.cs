using EntitySystem.Client.Components.Data.Record.List.Parameters;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.OrderLabel;

public class DataRecordListOrderLabelProcessor : IDataRecordListOrderLabelProcessor
{
    private readonly IDataRecordListOrderLabelFeature _orderLabelFeature;

    public DataRecordListOrderLabelProcessor(IDataRecordListOrderLabelFeature orderLabelFeature)
    {
        _orderLabelFeature = orderLabelFeature;
    }

    public void Process<TEntity>(IDataRecordListParameters<TEntity> parameters)
    {
        parameters.Features.AddFeature(_orderLabelFeature);
    }
}