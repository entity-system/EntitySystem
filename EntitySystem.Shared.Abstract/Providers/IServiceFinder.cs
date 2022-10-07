using System.Collections.Generic;

namespace EntitySystem.Shared.Abstract.Providers;

public interface IServiceFinder
{
    IEnumerable<T> Search<T>();
}