using EntitySystem.Client.Components.Data.Record.Info.Parameters;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature.BackButton;

public class DataRecordInfoBackButtonProcessor : IDataRecordInfoBackButtonProcessor
{
    private readonly IDataRecordInfoBackButtonFeature _backButtonFeature;

    public DataRecordInfoBackButtonProcessor(IDataRecordInfoBackButtonFeature backButtonFeature)
    {
        _backButtonFeature = backButtonFeature;
    }

    public void Process<TKey>(DataRecordInfoParameters<TKey> parameters)
    {
        if (parameters.Options.GetRecordInfo().GetRecordInfoSource().Features.GetFeature<DataRecordInfoEnableBackFeature<TKey>>() is not { } enabler) return;

        parameters.Features.AddFeature(enabler.Options);

        parameters.Features.AddFeature(_backButtonFeature);
    }
}