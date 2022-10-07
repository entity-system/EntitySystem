using EntitySystem.Client.Components.Data.Record.List.Parameters;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.Value;

public class DataRecordListValueProcessor : IDataRecordListValueProcessor
{
    private readonly IDataRecordListValueFeature _valueFeature;

    public DataRecordListValueProcessor(IDataRecordListValueFeature valueFeature)
    {
        _valueFeature = valueFeature;
    }

    public void Process<TKey>(IDataRecordListParameters<TKey> parameters)
    {
        parameters.Features.AddFeature(_valueFeature);
    }
}