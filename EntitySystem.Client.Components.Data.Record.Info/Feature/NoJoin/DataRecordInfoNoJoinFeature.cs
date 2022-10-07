using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature.NoJoin;

public class DataRecordInfoNoJoinFeature : IDataRecordInfoNoJoinFeature
{
    public IEnumerable<IRenderer> BuildTab<TKey>(BaseDataRecordInfo<TKey> recordInfo)
    {
        yield break;
    }

    public IEnumerable<IRenderer> BuildContent<TKey>(BaseDataRecordInfo<TKey> recordInfo)
    {
        if (recordInfo.Source.GetRecordInfoJoins().Any()) yield break;

        yield return new Renderer<BaseDataRecordInfo<TKey>, DataRecordInfoNoJoin<TKey>>(recordInfo);
    }
}