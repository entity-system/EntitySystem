using EntitySystem.Shared.Domain;

namespace EntitySystem.Shared.Extensions
{
    public static class EntityExtensions
    {
        public static bool IsNew(this IEntity entity)
        {
            return entity.Id == 0;
        }
    }
}
