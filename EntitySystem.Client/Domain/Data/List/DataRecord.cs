using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Components.Data.Output.Record;
using EntitySystem.Client.Components.Data.Output.Source;
using EntitySystem.Client.Components.Data.Record.Info.Source;
using EntitySystem.Client.Components.Data.Record.List.Value;
using EntitySystem.Client.Components.Data.Table.Record;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.List;

public class DataRecord<TKey> : Featured, IDataRecord<TKey>,
    IDataTableRecord<TKey>,
    IDataOutputRecord<TKey>
    where TKey : IEntity
{
    public IDataSource<TKey> Source { get; }

    public TKey Key { get; }

    public IDataObject<TKey> KeyObject { get; set; }

    public IList<IDataObject<TKey>> Objects { get; set; }

    public DataRecord(IDataSource<TKey> source, TKey key)
    {
        Source = source;
        Key = key;
    }

    public IDataRecordInfoSource<TKey> GetRecordInfoSource()
    {
        return Source.GetRecordInfoSource();
    }

    public IEnumerable<IDataRecordListValue> GetRecordListValues()
    {
        return Objects.SelectMany(o => o.GetValues());
    }

    public IDataOutputSource<TKey> GetOutputSource()
    {
        return Source.GetOutputSource();
    }

    /*public bool Selected { get; set; }*/

    /*public DataRecordAction Action { get; set; }*/

    /*public string Link { get; set; }

    public bool LinkBlank { get; set; }

    public long GetId()
    {
        return Key.Id;
    }*/
}