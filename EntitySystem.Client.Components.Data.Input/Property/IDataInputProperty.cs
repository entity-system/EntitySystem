using System;
using EntitySystem.Client.Abstract.Domain.Feature;

namespace EntitySystem.Client.Components.Data.Input.Property;

public interface IDataInputProperty : IFeatured
{
    long Priority { get; }

    string Name { get; }
}

public interface IDataInputProperty<in TEntity, TValue> : IDataInputProperty
       
{
    Func<TEntity, TValue> Getter { get; }

    Action<TEntity, TValue> Setter { get; }
}