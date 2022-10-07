using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EntitySystem.Shared.Abstract.Extensions;

namespace EntitySystem.Shared.Domain;

public interface IInitial<TEntity> : IEnumerable<TEntity>
    where TEntity : IEntity
{
    IEnumerable<(Func<TEntity> getter, Action<TEntity> setter)> GetAccessors();
}

public class Holder<TEntity>
{
    public class Envelope
    {
        public TEntity Entity { get; set; }
    }

    public List<Envelope> Envelopes { get; } = new();

    public int Add(TEntity entity)
    {
        var envelope = new Envelope { Entity = entity };

        Envelopes.Add(envelope);

        return Envelopes.Count - 1;
    }
}

public abstract class Initial<THolder, TEntity> : IInitial<TEntity>
    where THolder : Holder<TEntity>, new()
    where TEntity : IEntity
{
    public THolder Explicit { get; } = new();

    public IList<(Func<THolder, TEntity> getter, Action<THolder, TEntity> setter)> Access { get; private set; }

    protected Initial(IEnumerable<TEntity> list)
    {
        Initialize(list.Select(Express));
    }

    protected Initial(params Expression<Func<THolder, TEntity>>[] expressions)
    {
        Initialize(expressions);
    }

    private void Initialize(IEnumerable<Expression<Func<THolder, TEntity>>> expressions)
    {
        Access = expressions.Select(e => (e.Compile(), e.CompileSetter())).ToArray();
    }

    private Expression<Func<THolder, TEntity>> Express(TEntity entity)
    {
        var index = Explicit.Add(entity);

        return h => h.Envelopes[index].Entity;
    }

    public IEnumerable<(Func<TEntity> getter, Action<TEntity> setter)> GetAccessors()
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var (getter, setter) in Access)
        {
            yield return (() => getter(Explicit), e => setter(Explicit, e));
        }
    }

    public IEnumerable<TEntity> AllExcept(params TEntity[] entities) => this.Except(entities);

    public IEnumerator<TEntity> GetEnumerator()
    {
        return Access.Select(accessor => accessor.getter(Explicit)).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}