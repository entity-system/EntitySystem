using EntitySystem.Client.Components.Data.Record.Info.Parameters;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature.NoJoin;

public class DataRecordInfoNoJoinProcessor : IDataRecordInfoNoJoinProcessor
{
    private readonly IDataRecordInfoNoJoinFeature _noJoinFeature;

    public DataRecordInfoNoJoinProcessor(IDataRecordInfoNoJoinFeature noJoinFeature)
    {
        _noJoinFeature = noJoinFeature;
    }

    public void Process<TKey>(DataRecordInfoParameters<TKey> parameters)
    {
        parameters.Features.AddFeature(_noJoinFeature);
    }
}