using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature.BackButton;

public class DataRecordInfoBackButtonFeature : IDataRecordInfoBackButtonFeature
{
    public IEnumerable<IRenderer> Build<TKey>(BaseDataRecordInfo<TKey> recordInfo)
    {
        var parameters = new DataRecordInfoBackButtonParameters<TKey>(recordInfo);

        yield return new Renderer<DataRecordInfoBackButtonParameters<TKey>, DataRecordInfoBackButton<TKey>>(parameters);
    }
}