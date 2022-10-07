using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EntitySystem.Client.Components.Data.Input.Target.Source;

public interface IDataInputTargetSource<TEntity>
{
    Task<List<TEntity>> GetTargetHintsAsync(Expression<Func<TEntity, bool>> expression = null);
}