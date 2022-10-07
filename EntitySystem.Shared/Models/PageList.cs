using System.Collections.Generic;

namespace EntitySystem.Shared.Models;

public class PageList<TEntity>
{
    public List<TEntity> Page { get; set; }

    public int MasterCount { get; set; }
}