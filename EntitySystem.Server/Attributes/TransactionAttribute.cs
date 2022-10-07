using System;

namespace EntitySystem.Server.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class TransactionAttribute : SessionAttribute
{
}