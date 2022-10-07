using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature.DeleteButton;

public class DataRecordInfoDeleteButtonFeature : IDataRecordInfoDeleteButtonFeature
{
    public const long Priority = 20;

    public IEnumerable<IRenderer> Build<TKey>(BaseDataRecordInfo<TKey> recordInfo)
    {
        yield return new Renderer<BaseDataRecordInfo<TKey>, DataRecordInfoDeleteButton<TKey>>(recordInfo, Priority);
    }
}