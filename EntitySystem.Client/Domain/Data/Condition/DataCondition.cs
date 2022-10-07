using System;
using System.Linq.Expressions;
using EntitySystem.Client.Components.Data.Record.List.Condition;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Domain;
using EntitySystem.Shared.Query;

namespace EntitySystem.Client.Domain.Data.Condition;

public class DataCondition<TEntity> : IDataCondition,
    IDataRecordListCondition
    where TEntity : IEntity
{
    public Expression<Func<TEntity, bool>> Condition { get; }

    public string Name { get; }

    public bool Like { get; set; }

    public long Priority { get; set; }

    private Guid _guid;

    public Guid Guid
    {
        get => Source?.Guid ?? _guid;
        set => _guid = value;
    }

    public IDataSource<TEntity> Source { get; private set; }

    public DataCondition(Expression<Func<TEntity, bool>> condition, string name = null)
    {
        Condition = condition;
        Name = name;
    }

    public DataCondition(Guid guid, Expression<Func<TEntity, bool>> condition)
    {
        Guid = guid;
        Condition = condition;
    }

    public void Assign(IDataSource<TEntity> source)
    {
        if (Source != null) throw new InvalidOperationException("Condition already assigned.");

        Source = source;

        Priority = DateTime.Now.Ticks;

        Source.Conditions.Add(this);
    }

    public QueryCondition GetQueryCondition()
    {
        if (Condition is not LambdaExpression lambda) throw new InvalidOperationException("Invalid condition, must be lambda");

        if (lambda.Body is not BinaryExpression binary) throw new InvalidOperationException("Invalid condition, must be binary");

        if (binary.Left is not MemberExpression and not ParameterExpression) throw new InvalidOperationException("Invalid condition, left must be member or parameter");

        var target = QueryTarget.From(Guid, binary.Left);

        var constant = binary.Right switch
        {
            ConstantExpression value => QueryConstant.From(value),
            MemberExpression variable => QueryConstant.From(variable),
            _ => throw new InvalidOperationException("Invalid condition, right must be constant or variable")
        };

        var queryCondition = new QueryCondition { Priority = Priority };

        if (binary.NodeType == ExpressionType.Equal)
        {
            if (Like) queryCondition.Like = new QueryLike(target, constant);

            else queryCondition.Equal = new QueryEqual(target, constant);
        }

        else throw new InvalidOperationException("Invalid condition, not supported");

        return queryCondition;
    }

    public void UnAssign()
    {
        Source.Conditions.Remove(this);

        Source = null;
    }

    public static QueryCondition Query(Guid guid, Expression<Func<TEntity, bool>> condition)
    {
        return new DataCondition<TEntity>(guid, condition).GetQueryCondition();
    }
}