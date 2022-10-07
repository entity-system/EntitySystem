using EntitySystem.Shared.Query;

namespace EntitySystem.Client.Domain.Data.Condition;

public interface IDataCondition
{
    string Name { get; }

    QueryCondition GetQueryCondition();

    long Priority { get; }

    void UnAssign();

}