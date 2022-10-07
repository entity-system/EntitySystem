using EntitySystem.Client.Components.Data.Record.List.Parameters;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.EditButton;

public class DataRecordListEditButtonProcessor : IDataRecordListEditButtonProcessor
{
    private readonly IDataRecordListEditButtonFeature _editButtonFeature;

    public DataRecordListEditButtonProcessor(IDataRecordListEditButtonFeature editButtonFeature)
    {
        _editButtonFeature = editButtonFeature;
    }

    public void Process<TEntity>(IDataRecordListParameters<TEntity> parameters)
    {
        var source = parameters.Options.GetRecordListSource();

        if (source.Features.HasFeature<DataRecordListDisableEditFeature>()) return;

        parameters.Features.AddFeature(_editButtonFeature);
    }
}