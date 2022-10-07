using EntitySystem.Client.Components.Data.Record.Info.Parameters;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature.DeleteButton;

public class DataRecordInfoDeleteButtonProcessor : IDataRecordInfoDeleteButtonProcessor
{
    private readonly IDataRecordInfoDeleteButtonFeature _deleteButtonFeature;

    public DataRecordInfoDeleteButtonProcessor(IDataRecordInfoDeleteButtonFeature deleteButtonFeature)
    {
        _deleteButtonFeature = deleteButtonFeature;
    }

    public void Process<TKey>(DataRecordInfoParameters<TKey> parameters)
    {
        if (parameters.Options.GetRecordInfo().GetRecordInfoSource().Features.HasFeature<DataRecordInfoDisableDeleteFeature>()) return;

        parameters.Features.AddFeature(_deleteButtonFeature);
    }
}