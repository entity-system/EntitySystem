using EntitySystem.Client.Components.Data.Record.List.Parameters;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.RemoveButton;

public class DataRecordListRemoveButtonProcessor : IDataRecordListRemoveButtonProcessor
{
    private readonly IDataRecordListRemoveButtonFeature _removeButtonFeature;

    public DataRecordListRemoveButtonProcessor(IDataRecordListRemoveButtonFeature removeButtonFeature)
    {
        _removeButtonFeature = removeButtonFeature;
    }

    public void Process<TEntity>(IDataRecordListParameters<TEntity> parameters)
    {
        var source = parameters.Options.GetRecordListSource();

        if (source.Features.HasFeature<DataRecordListDisableRemoveFeature>()) return;

        parameters.Features.AddFeature(_removeButtonFeature);
    }
}