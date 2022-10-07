using EntitySystem.Client.Components.Data.Record.Info.Parameters;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature.JoinTab;

public class DataRecordInfoJoinTabProcessor : IDataRecordInfoJoinTabProcessor
{
    private readonly IDataRecordInfoJoinTabFeature _joinTabFeature;

    public DataRecordInfoJoinTabProcessor(IDataRecordInfoJoinTabFeature joinTabFeature)
    {
        _joinTabFeature = joinTabFeature;
    }

    public void Process<TKey>(DataRecordInfoParameters<TKey> parameters)
    {
        parameters.Features.AddFeature(_joinTabFeature);
    }
}