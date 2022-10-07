using System;

namespace EntitySystem.Server.Extensions;

public static class GuidExtensions
{
    public static string ToSqlAlias(this Guid guid)
    {
        return guid.ToString().Replace("-", string.Empty);
    }
}