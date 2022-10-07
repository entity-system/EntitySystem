using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace EntitySystem.Server.Domain;

public class GuidStore<T> : IEnumerable<T>
{
    public ConcurrentDictionary<Guid, T> Store { get; set; } = new();

    public Guid Add(T entity)
    {
        var guid = Guid.NewGuid();

        Store[guid] = entity;

        return guid;
    }

    public T Peek(Guid guid)
    {
        var entity = Store[guid];

        return entity;
    }

    public T Pop(Guid guid)
    {
        var entity = Store[guid];

        Store.Remove(guid, out _);

        return entity;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return Store.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}