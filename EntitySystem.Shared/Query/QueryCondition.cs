namespace EntitySystem.Shared.Query;

public class QueryCondition
{
    public long Priority { get; set; }

    public QueryEqual Equal { get; set; }

    public QueryLike Like { get; set; }

    public IQueryCondition GetCondition()
    {
        if (Equal != null) return Equal;
        
        return Like; 
        
    }

    public override string ToString()
    {
        return $"{GetCondition()}";
    }
}