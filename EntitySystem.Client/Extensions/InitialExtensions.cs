using System;
using System.Collections.Generic;
using System.Linq;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Extensions;

    public static class InitialExtensions
    {
        public static void LoadFrom<TEntity, TValue>(this IInitial<TEntity> initial, IReadOnlyList<TEntity> collection, Func<TEntity, TValue> comparer)
            where TEntity : IEntity
        {
            foreach (var (getter, setter) in initial.GetAccessors())
            {
                var current = getter();

                var replace = collection.FirstOrDefault(e => Equals(comparer(e), comparer(current)));

                if (replace != null) setter(replace);
            }
        }
    }

