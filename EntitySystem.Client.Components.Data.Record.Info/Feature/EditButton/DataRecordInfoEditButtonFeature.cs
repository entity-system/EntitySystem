using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature.EditButton;

public class DataRecordInfoEditButtonFeature : IDataRecordInfoEditButtonFeature
{
    public const long Priority = 10;

    public IEnumerable<IRenderer> Build<TKey>(BaseDataRecordInfo<TKey> recordInfo)
    {
        yield return new Renderer<BaseDataRecordInfo<TKey>, DataRecordInfoEditButton<TKey>>(recordInfo, Priority);
    }
}