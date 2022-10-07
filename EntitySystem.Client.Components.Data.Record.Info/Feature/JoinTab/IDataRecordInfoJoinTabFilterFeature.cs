using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Components.Data.Record.Info.Join;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature.JoinTab;

public interface IDataRecordInfoJoinTabFilterFeature : IFeature
{
    bool IsHidden<TKey>(IDataRecordInfoJoin<TKey> parameters);
}