using System.Collections.Generic;
using System.Threading.Tasks;
using EntitySystem.Client.Components.Data.Record.Info.Join;
using EntitySystem.Client.Domain.Data.List;
using EntitySystem.Client.Domain.Data.Pair;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Join;

public interface IDataJoin : IDataPair
{
    Task RequestNestedAsync();
}

public interface IDataJoin<in TParent> : IDataJoin
    where TParent : IEntity
{
    IEnumerable<IDataObject<TKey>> CreateObjectsNested<TKey>(DataRecord<TKey> record, IEnumerable<TParent> parents) where TKey : IEntity;

    IDataRecordInfoJoin<TParent> GetRecordInfoJoin();
}

public interface IDataJoin<TParent, TChild> : IDataJoin<TParent>, IDataPair<TParent, TChild>
    where TParent : IEntity
    where TChild : IEntity
{

}