using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Parameters;
using EntitySystem.Client.Components.Data.Record.Info.Join;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature.JoinTab;

public class DataRecordInfoJoinTabParameters<TKey> : Featured, IParameters
{
    public int Index { get; set; }

    public IDataRecordInfoJoin<TKey> Join { get; }

    public BaseDataRecordInfo<TKey> RecordInfo { get; }

    public DataRecordInfoJoinTabParameters(int index, IDataRecordInfoJoin<TKey> join, BaseDataRecordInfo<TKey> recordInfo)
    {
        Index = index;
        Join = join;
        RecordInfo = recordInfo;
    }
}