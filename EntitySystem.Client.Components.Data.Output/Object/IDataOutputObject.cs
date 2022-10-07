using System.Collections.Generic;
using EntitySystem.Client.Components.Data.Output.Record;

namespace EntitySystem.Client.Components.Data.Output.Object;

public interface IDataOutputObject<TKey, TEntity>
{
    IDataOutputRecord<TKey> GetOutputRecord();

    IList<TEntity> Entities { get; }
}