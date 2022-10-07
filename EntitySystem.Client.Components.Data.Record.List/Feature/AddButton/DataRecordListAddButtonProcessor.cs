using EntitySystem.Client.Components.Data.Record.List.Parameters;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.AddButton;

public class DataRecordListAddButtonProcessor : IDataRecordListAddButtonProcessor
{
    private readonly IDataRecordListAddButtonFeature _addButtonFeature;

    public DataRecordListAddButtonProcessor(IDataRecordListAddButtonFeature addButtonFeature)
    {
        _addButtonFeature = addButtonFeature;
    }

    public void Process<TEntity>(IDataRecordListParameters<TEntity> parameters)
    {
        var source = parameters.Options.GetRecordListSource();

        if (source.Features.HasFeature<DataRecordListDisableAddFeature>()) return;

        parameters.Features.AddFeature(_addButtonFeature);
    }
}