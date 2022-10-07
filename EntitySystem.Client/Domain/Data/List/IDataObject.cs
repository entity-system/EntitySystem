using System.Collections.Generic;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.List;

public interface IDataObject<TKey, TEntity> : IDataObject<TKey>
    where TKey : IEntity
    where TEntity : IEntity
{
    IDataSource<TEntity> Source { get; }

    IList<TEntity> Entities { get; }
}

public interface IDataObject<TKey> : IDataObject
    where TKey : IEntity
{
    /*IDataRecord<TKey> Record { get; }*/

    /*IEnumerable<IDataKeyValue<TKey>> GetValues();*/

    IDataRecord<TKey> GetRecord();
}

public interface IDataObject
{
    IEnumerable<IDataValue> GetValues();
}