using EntitySystem.Client.Components.Data.Record.List.Parameters;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.Title;

public class DataRecordListTitleProcessor : IDataRecordListTitleProcessor
{
    private readonly IDataRecordListTitleFeature _titleFeature;

    public DataRecordListTitleProcessor(IDataRecordListTitleFeature titleFeature)
    {
        _titleFeature = titleFeature;
    }

    public void Process<TEntity>(IDataRecordListParameters<TEntity> parameters)
    {
        var source = parameters.Options.GetRecordListSource();

        if (source.Features.HasFeature<DataRecordListDisableTitleFeature>()) return;

        parameters.Features.AddFeature(_titleFeature);
    }
}