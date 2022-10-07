using System;
using System.Collections.Generic;

namespace EntitySystem.Shared.Query;

public class QueryOrderDescending : IQueryOrder
{
    public QueryTarget Target { get; set; }

    public QueryOrderDescending Clone(IDictionary<Guid, Guid> mapping = null)
    {
        return new QueryOrderDescending
        {
            Target = Target?.Clone(mapping)
        };
    }
}