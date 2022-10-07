using System;

namespace EntitySystem.Shared.Domain;

public interface IUnique
{
    Guid Guid { get; }
}