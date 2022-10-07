using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature.JoinTab;

public class DataRecordInfoJoinTabFeature : IDataRecordInfoJoinTabFeature
{
    public IEnumerable<IRenderer> BuildTab<TKey>(BaseDataRecordInfo<TKey> recordInfo)
    {
        return recordInfo.Source
            .GetRecordInfoJoins()
            .Where(j => !j.Features.GetFeatures<IDataRecordInfoJoinTabFilterFeature>().Any(f => f.IsHidden(j)))
            .Select((j, i) => new DataRecordInfoJoinTabParameters<TKey>(i, j, recordInfo))
            .Select(p => new Renderer<DataRecordInfoJoinTabParameters<TKey>, DataRecordInfoJoinTab<TKey>>(p, p.Index));
    }

    public IEnumerable<IRenderer> BuildContent<TKey>(BaseDataRecordInfo<TKey> recordInfo)
    {
        var join = recordInfo.Source
            .GetRecordInfoJoins()
            .Where(j => !j.Features.GetFeatures<IDataRecordInfoJoinTabFilterFeature>().Any(f => f.IsHidden(j)))
            .ElementAtOrDefault(recordInfo.AnchorIndex);

        if(join == null) yield break;

        yield return join.BuildTableJoin(recordInfo);
    }
}