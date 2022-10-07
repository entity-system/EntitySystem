using System;
using System.Linq.Expressions;
using EntitySystem.Client.Components.Data.Record.List.Order;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Domain;
using EntitySystem.Shared.Query;

namespace EntitySystem.Client.Domain.Data.Order;

public class DataOrder<TEntity, TValue> : IDataOrder<TEntity>,
    IDataRecordListOrder
    where TEntity : IEntity
{
    public Expression<Func<TEntity, TValue>> Expression { get; }

    public bool Descending { get; }

    public string Name { get; }

    public long Priority { get; set; }

    private Guid _guid;

    public Guid Guid 
    { 
        get => Source?.Guid ?? _guid;
        set => _guid = value;
    }

    public IDataSource<TEntity> Source { get; private set; }

    public DataOrder(Expression<Func<TEntity, TValue>> expression, bool descending = false, string name = null)
    {
        Expression = expression;
        Descending = descending;
        Name = name;
    }

    public DataOrder(Guid guid, Expression<Func<TEntity, TValue>> expression, bool descending = false)
    {
        Guid = guid;
        Expression = expression;
        Descending = descending;
    }

    public void Assign(IDataSource<TEntity> source)
    {
        if (Source != null) throw new InvalidOperationException("OrderBy already assigned.");

        Source = source;

        Priority = DateTime.Now.Ticks;

        Source.Orders.Add(this);
    }

    public QueryOrder GetQueryOrder()
    {
        if (Expression.Body is not MemberExpression member) throw new InvalidOperationException("Invalid OrderBy expression");

        var target = QueryTarget.From(Guid, member);

        var queryOrder = new QueryOrder { Priority = Priority };

        if (Descending) queryOrder.Descending = new QueryOrderDescending { Target = target };

        else queryOrder.Ascending = new QueryOrderAscending { Target = target };

        return queryOrder;
    }

    public void UnAssign()
    {
        Source.Orders.Remove(this);

        Source = null;
    }

    public IDataRecordListOrder GetRecordListOrder()
    {
        return this;
    }

    public static QueryOrder Query(Guid guid, Expression<Func<TEntity, TValue>> condition, bool descending)
    {
        return new DataOrder<TEntity, TValue>(guid, condition, descending).GetQueryOrder();
    }
}