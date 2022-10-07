using System;

namespace EntitySystem.Shared.Abstract.Services;

public class TimeService : ITimeService
{
    public DateTime GetTimeNow()
    {
        return DateTime.UtcNow;
    }
}