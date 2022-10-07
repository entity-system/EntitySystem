using System;

namespace EntitySystem.Client.Components.Data.Output.Property;

public interface IDataOutputProperty<in TEntity, out TValue>
{
    Func<TEntity, TValue> Getter { get; }

    long Priority { get; }

    string ToString(TEntity entity);
}