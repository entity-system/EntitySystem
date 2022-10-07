namespace EntitySystem.Shared.Query;

public class QueryOrder
{
    public long Priority { get; set; }

    public QueryOrderAscending Ascending { get; set; }

    public QueryOrderDescending Descending { get; set; }

    public IQueryOrder GetOrder()
    {
        if (Ascending != null) return Ascending;
        
        return Descending;
    }
}