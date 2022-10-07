using System;
using System.Collections.Generic;

namespace EntitySystem.Shared.Query;

public class QueryLike : IQueryCondition
{
    public QueryTarget Target { get; set; }

    public QueryConstant Constant { get; set; }

    public QueryLike()
    {
    }

    public QueryLike(QueryTarget target, QueryConstant constant)
    {
        Target = target;
        Constant = constant;
    }

    public QueryLike Clone(IDictionary<Guid, Guid> mapping = null)
    {
        return new QueryLike
        {
            Target = Target?.Clone(mapping),
            Constant = Constant?.Clone(mapping)
        };
    }

    public IEnumerable<IQueryParameter> GetParameters()
    {
        yield return Target;

        yield return Constant;
    }

    public override string ToString()
    {
        return $"({Target} LIKE '%{Constant}%')";
    }
}