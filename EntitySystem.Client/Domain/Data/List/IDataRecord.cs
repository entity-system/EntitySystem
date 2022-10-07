using System.Collections.Generic;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.List;

public interface IDataRecord<TKey>
    where TKey : IEntity
{
    IDataSource<TKey> Source { get; }

    TKey Key { get; }

    IList<IDataObject<TKey>> Objects { get; }

    /*bool Selected { get; set; }

    string Link { get; set; }

    bool LinkBlank { get; set; }*/

    /*DataRecordAction Action { get; set; }*/

    //long GetId();
}