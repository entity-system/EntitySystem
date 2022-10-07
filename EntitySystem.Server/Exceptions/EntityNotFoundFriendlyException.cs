namespace EntitySystem.Server.Exceptions;

public class EntityNotFoundFriendlyException<TEntity> : EntityActionFriendlyException<TEntity>, IEntityNotFoundFriendlyException
{
    public EntityNotFoundFriendlyException(string action, string notification, string description) : base(action, notification, CreateCouldNotBeFoundMessage(description))
    {
    }

    public EntityNotFoundFriendlyException(string action, string notification, long id) : base(action, notification, CreateCouldNotBeFoundMessage(id))
    {
    }

    public const string ZeroIdMessage = "entity with zero id (0)";

    public const string NonZeroIdMessage = "entity with id ({0})";

    public static string CreateCouldNotBeFoundMessage(long id)
    {
        var template = id switch
        {
            0 => ZeroIdMessage,
            _ => NonZeroIdMessage
        };

        return CreateCouldNotBeFoundMessage(string.Format(template, id));
    }

    public const string CouldNotBeFoundMessage = "{0} could not be found";

    public static string CreateCouldNotBeFoundMessage(string description)
    {
        return string.Format(CouldNotBeFoundMessage, description);
    }
}