using System.Net;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Shared.Extensions
{
    public static class HttpStatusCodeExtensions
    {
        public static bool IsClientRedirect(this HttpStatusCode statusCode)
        {
            return (CustomHttpStatusCode)statusCode switch
            {
                CustomHttpStatusCode.ClientRedirect => true,
                _ => false
            };
        }

        public static bool IsFriendlyException(this HttpStatusCode statusCode)
        {
            return (CustomHttpStatusCode)statusCode switch
            {
                CustomHttpStatusCode.FriendlyBadRequest => true,
                CustomHttpStatusCode.FriendlyNotFound => true,
                _ => false
            };
        }
    }
}
