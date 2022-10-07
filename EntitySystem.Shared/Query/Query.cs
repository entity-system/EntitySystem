using System.Collections.Generic;

namespace EntitySystem.Shared.Query;

public class Query
{
    public int Skip { get; set; }

    public int Take { get; set; } = 100;

    public List<QueryOrder> Orders { get; set; } = new();

    public List<QueryCondition> Conditions { get; set; } = new();
}