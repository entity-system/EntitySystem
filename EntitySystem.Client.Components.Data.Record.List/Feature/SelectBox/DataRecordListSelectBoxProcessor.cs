using EntitySystem.Client.Components.Data.Record.List.Parameters;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.SelectBox;

public class DataRecordListSelectBoxProcessor : IDataRecordListSelectBoxProcessor
{
    private readonly IDataRecordListSelectBoxFeature _selectBoxFeature;

    public DataRecordListSelectBoxProcessor(IDataRecordListSelectBoxFeature selectBoxFeature)
    {
        _selectBoxFeature = selectBoxFeature;
    }

    public void Process<TKey>(IDataRecordListParameters<TKey> parameters)
    {
        var source = parameters.Options.GetRecordListSource();

        if (source.Features.HasFeature<DataRecordLisDisableSelectFeature>()) return;

        parameters.Features.AddFeature(_selectBoxFeature);
    }
}