using System;
using System.Threading.Tasks;
using EntitySystem.Shared.Domain;
using EntitySystem.Shared.Exceptions;
using EntitySystem.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntitySystem.Server.Exceptions
{
    public class ExceptionHandler
    {
        public static async Task<IActionResult> TryAsync(Task<IActionResult> task)
        {
            try
            {
                return await task;
            }
            catch (Exception e) when (e is IEntityBadFriendlyException badEntityException)
            {
                return CreateFriendlyExceptionJsonResult(e.Message, badEntityException.Notification, (int)CustomHttpStatusCode.FriendlyBadRequest);
            }
            catch (Exception e) when (e is IEntityNotFoundFriendlyException notFoundEntityException)
            {
                return CreateFriendlyExceptionJsonResult(e.Message, notFoundEntityException.Notification, (int)CustomHttpStatusCode.FriendlyNotFound);
            }
            catch (Exception e) when (e is IClientRedirectException redirect)
            {
                return CreateClientRedirectJsonResult(redirect);
            }

            // ReSharper disable once RedundantCatchClause
#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
            {
                throw;
            }
#pragma warning restore CS0168 // Variable is declared but never used
#pragma warning restore IDE0059 // Unnecessary assignment of a value
        }

        private static JsonResult CreateClientRedirectJsonResult(IClientRedirectException redirect)
        {
            return new JsonResult(new ClientRedirectModel { Uri = redirect.Uri, Message = redirect.Message, ClientSide = redirect.ClientSide }) { StatusCode = (int)CustomHttpStatusCode.ClientRedirect };
        }

        private static JsonResult CreateFriendlyExceptionJsonResult(string message, string notification, int statusCode)
        {
            return new JsonResult(new FriendlyExceptionModel { Message = message, Notification = notification }) { StatusCode = statusCode };
        }
    }
}
