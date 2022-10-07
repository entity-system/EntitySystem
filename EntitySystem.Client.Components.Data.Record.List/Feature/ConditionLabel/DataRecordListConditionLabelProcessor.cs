using EntitySystem.Client.Components.Data.Record.List.Parameters;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.ConditionLabel;

public class DataRecordListConditionLabelProcessor : IDataRecordListConditionLabelProcessor
{
    private readonly IDataRecordListConditionLabelFeature _conditionLabelFeature;

    public DataRecordListConditionLabelProcessor(IDataRecordListConditionLabelFeature conditionLabelFeature)
    {
        _conditionLabelFeature = conditionLabelFeature;
    }

    public void Process<TEntity>(IDataRecordListParameters<TEntity> parameters)
    {
        parameters.Features.AddFeature(_conditionLabelFeature);
    }
}