using System;
using System.Collections.Generic;

namespace EntitySystem.Shared.Query;

public class QueryEqual : IQueryCondition
{
    public QueryParameter Left { get; set; }

    public QueryParameter Right { get; set; }

    public QueryEqual()
    {
    }

    public QueryEqual(QueryTarget target, QueryConstant constant) : this(new QueryParameter { Target = target }, new QueryParameter { Constant = constant })
    {
    }

    public QueryEqual(QueryParameter left, QueryParameter right)
    {
        Left = left;
        Right = right;
    }

    public QueryEqual Clone(IDictionary<Guid, Guid> mapping = null)
    {
        return new QueryEqual
        {
            Left = Left?.Clone(mapping),
            Right = Right?.Clone(mapping)
        };
    }

    public IEnumerable<IQueryParameter> GetParameters()
    {
        yield return Left.GetParameter();

        yield return Right.GetParameter();
    }

    public override string ToString()
    {
        return $"({Left} == {Right})";
    }
}