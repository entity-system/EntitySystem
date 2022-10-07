using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntitySystem.Server.Attributes;
using EntitySystem.Server.Services;
using EntitySystem.Shared.Abstract.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EntitySystem.Server.Filters;

public class DatabaseActionFilter : IAsyncActionFilter
{
    private readonly DatabaseService _databaseService;
    private readonly List<IFinalService> _finalServices;

    public DatabaseActionFilter(DatabaseService databaseService, IServiceFinder serviceFinder)
    {
        _databaseService = databaseService;
        _finalServices = serviceFinder.Search<IFinalService>().ToList();
    }

    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionDescriptor is not ControllerActionDescriptor controllerActionDescriptor) return Execute();

        var actionAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(true);

        if (!actionAttributes.OfType<SessionAttribute>().Any()) return Execute();

        _databaseService.OpenSession();

        if (!actionAttributes.OfType<TransactionAttribute>().Any()) return Execute();

        _databaseService.BeginTransaction();

        return Execute(c =>
        {
            if (c.Exception == null && c.Result is OkResult or ObjectResult { StatusCode: < 400 or null } or ContentResult { StatusCode: < 400 or null })
            {
                _databaseService.CommitTransaction();

                return true;
            }

            _databaseService.RollbackTransaction();

            return false;
        });

        async Task Execute(Func<ActionExecutedContext, bool> action = null)
        {
            var executedContext = await next.Invoke();

            var success = action?.Invoke(executedContext) ?? true;

            foreach (var service in _finalServices) await service.ProcessAsync(success);
        }
    }
}