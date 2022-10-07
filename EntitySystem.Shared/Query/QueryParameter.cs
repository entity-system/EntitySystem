using System;
using System.Collections.Generic;

namespace EntitySystem.Shared.Query;

public class QueryParameter
{
    public QueryTarget Target { get; set; }

    public QueryConstant Constant { get; set; }

    public QueryParameter Clone(IDictionary<Guid, Guid> mapping = null)
    {
        return new QueryParameter
        {
            Target = Target?.Clone(mapping),
            Constant = Constant?.Clone(mapping)
        };
    }

    public IQueryParameter GetParameter()
    {
        if (Target != null) return Target;

        return Constant;
    }

    public override string ToString()
    {
        return $"{GetParameter()}";
    }
}