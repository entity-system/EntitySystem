using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Parameters;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature.BackButton;

public class DataRecordInfoBackButtonParameters<TKey> : Featured, IParameters
{
    public DataRecordInfoBackButtonOptions<TKey> Options { get; }

    public BaseDataRecordInfo<TKey> RecordInfo { get; }

    public DataRecordInfoBackButtonParameters(BaseDataRecordInfo<TKey> recordInfo)
    {
        Options = recordInfo.Parameters.Features.GetFeature<DataRecordInfoBackButtonOptions<TKey>>();
        RecordInfo = recordInfo;
    }

    public string Link => Options.LinkFactory(RecordInfo.Record.Key);
}