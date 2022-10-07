using System;

namespace EntitySystem.Shared.Abstract.Services;

public interface ITimeService
{
    DateTime GetTimeNow();
}