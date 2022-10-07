using System.Collections.Generic;

namespace EntitySystem.Shared.Query;

public class QueryList<TEntity>
{
    public List<TEntity> Page { get; set; }

    public int MasterCount { get; set; }
}