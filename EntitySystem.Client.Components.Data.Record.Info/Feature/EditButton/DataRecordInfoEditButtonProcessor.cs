using EntitySystem.Client.Components.Data.Record.Info.Parameters;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature.EditButton;

public class DataRecordInfoEditButtonProcessor : IDataRecordInfoEditButtonProcessor
{
    private readonly IDataRecordInfoEditButtonFeature _editButtonFeature;

    public DataRecordInfoEditButtonProcessor(IDataRecordInfoEditButtonFeature editButtonFeature)
    {
        _editButtonFeature = editButtonFeature;
    }

    public void Process<TKey>(DataRecordInfoParameters<TKey> parameters)
    {
        if (parameters.Options.GetRecordInfo().GetRecordInfoSource().Features.HasFeature<DataRecordInfoDisableEditFeature>()) return;

        parameters.Features.AddFeature(_editButtonFeature);
    }
}