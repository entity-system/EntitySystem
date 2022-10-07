using System.Collections.Generic;

namespace EntitySystem.Shared.Query;

public interface IQueryCondition
{
    IEnumerable<IQueryParameter> GetParameters();
}