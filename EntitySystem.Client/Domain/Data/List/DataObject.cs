using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Components.Data.Output.Object;
using EntitySystem.Client.Components.Data.Output.Record;
using EntitySystem.Client.Domain.Data.Property;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.List;

public class DataObject<TKey, TEntity> : IDataObject<TKey, TEntity>,
    IDataOutputObject<TKey, TEntity>
    where TKey : IEntity
    where TEntity : IEntity
{
    public IDataSource<TEntity> Source { get; }

    public DataRecord<TKey> Record { get; }

    public IList<TEntity> Entities { get; }

    public IList<IDataProperty<TEntity>> Properties { get; }

    public DataObject(IDataSource<TEntity> source, DataRecord<TKey> record, IList<TEntity> entities, IList<IDataProperty<TEntity>> properties)
    {
        Source = source;
        Record = record;
        Entities = entities;
        Properties = properties;
    }

    public IDataRecord<TKey> GetRecord()
    {
        return Record;
    }

    public IEnumerable<IDataValue> GetValues()
    {
        return Properties.Select(p => p.GetValue(this));
    }

    public IDataOutputRecord<TKey> GetOutputRecord()
    {
        return Record;
    }
}