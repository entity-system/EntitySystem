using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Record.List.Feature.ConditionLabel;

internal class DataRecordListConditionLabelFeature : IDataRecordListConditionLabelFeature
{
    public const long Priority = 60;

    public IEnumerable<IRenderer> Build<TKey>(BaseDataRecordList<TKey> recordList)
    {
        return recordList.Source.GetRecordListConditionsNested()
            .Where(c => !string.IsNullOrEmpty(c.Name))
            .Select(c => new DataRecordListConditionParameters<TKey>(recordList, c))
            .Select(p => new Renderer<DataRecordListConditionParameters<TKey>, DataRecordListConditionLabel<TKey>>(p, Priority));
    }
}