using System;
using System.Collections.Generic;

namespace EntitySystem.Shared.Query;

public class QueryOrderAscending : IQueryOrder
{
    public QueryTarget Target { get; set; }

    public QueryOrderAscending Clone(IDictionary<Guid, Guid> mapping = null)
    {
        return new QueryOrderAscending
        {
            Target = Target?.Clone(mapping)
        };
    }
}