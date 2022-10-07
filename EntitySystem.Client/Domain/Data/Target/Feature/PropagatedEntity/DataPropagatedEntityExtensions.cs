using System;
using System.Linq;
using System.Linq.Expressions;
using EntitySystem.Client.Domain.Data.Pair;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Abstract.Extensions;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Target.Feature.PropagatedEntity;

public static class DataPropagatedEntityExtensions
{
    public static void Propagate(this IDataSource source, IEntity entity, Action<IEntity> action)
    {
        action(entity);

        foreach (var target in source.GetPairs().OfType<IDataTarget>()) target.Propagate(entity, action);
    }

    public static void Propagate(this IDataTarget target, IEntity entity, Action<IEntity> action)
    {
        var source = target.GetChildSource();

        foreach (var paired in target.GetPairedEntities(entity)) source.Propagate(paired, action);
    }

    public static void Propagate<TParent, TChild, TValue, TType>(this IDataPair<TParent, TChild> pair, TParent parent, TChild child, Expression<Func<TType, TValue>> expression)
        where TParent : IEntity
        where TChild : IEntity
    {
        var getter = expression.Compile();

        var setter = expression.CompileSetter();

        if (parent is TType parentType && getter(parentType) is { } parentValue) pair.GetChildEntitySource().Propagate(child, Propagator(parentValue));

        if (child is TValue childValue) pair.GetParentEntitySource().Propagate(parent, Propagator(childValue));

        Action<IEntity> Propagator(TValue value)
        {
            return e =>
            {
                if (e is not TType type || value.Equals(getter(type))) return;

                setter(type, value);
            };
        }
    }
}