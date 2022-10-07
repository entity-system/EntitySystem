using System;
using EntitySystem.Shared.Models;

namespace EntitySystem.Shared.Exceptions;

public class FriendlyException : Exception, IFriendlyException
{
    public virtual string Notification { get; }

    public override string Message { get; }

    public FriendlyException(FriendlyExceptionModel model) : this(model.Notification, model.Message)
    {
    }

    public FriendlyException(string notification, string message) : base(message)
    {
        Notification = notification;

        Message = message;
    }
}