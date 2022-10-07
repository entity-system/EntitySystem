using System;
using System.Linq.Expressions;
using EntitySystem.Client.Components.Data.Input.Property;

namespace EntitySystem.Client.Components.Data.Input.Text.Property;

public interface IDataInputTextProperty<TEntity> : IDataInputProperty<TEntity, string>
{
    Expression<Func<TEntity, string>> Expression { get; }
}