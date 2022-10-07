using EntitySystem.Client.Components.Data.Record.Info.Join;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature.JoinTab
{
    public class DataRecordInfoDisableJoinTabFeature : IDataRecordInfoJoinTabFilterFeature
    {
        public bool IsHidden<TKey>(IDataRecordInfoJoin<TKey> parameters)
        {
            return true;
        }
    }
}