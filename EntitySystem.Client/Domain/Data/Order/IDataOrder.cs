using EntitySystem.Client.Components.Data.Record.List.Order;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Domain;
using EntitySystem.Shared.Query;

namespace EntitySystem.Client.Domain.Data.Order;

public interface IDataOrder<TEntity> : IDataOrder
    where TEntity : IEntity
{
    IDataSource<TEntity> Source { get; }

    QueryOrder GetQueryOrder();

    void Assign(IDataSource<TEntity> source);
}

public interface IDataOrder
{
    long Priority { get; set; }

    string Name { get; }

    bool Descending { get; }

    void UnAssign();

    IDataRecordListOrder GetRecordListOrder();
}