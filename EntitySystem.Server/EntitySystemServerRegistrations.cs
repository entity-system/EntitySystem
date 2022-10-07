using EntitySystem.Server.Filters;
using EntitySystem.Server.Services;
using EntitySystem.Shared.Abstract.Providers;
using EntitySystem.Shared.Abstract.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EntitySystem.Server;

public static class EntitySystemServerRegistrations
{
    public static IServiceCollection AddEntitySystemServer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddControllersWithViews(o => o.Filters.Add<DatabaseActionFilter>());

        serviceCollection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return serviceCollection
            .AddSingleton<IDescriptorProvider>(new DescriptorProvider(serviceCollection))
            .AddScoped<IServiceFinder, ServiceFinder>()
            .AddSingleton<ITimeService, TimeService>()
            .AddScoped<DatabaseService>()
            .AddSingleton<QueryStoreService>()
            .AddScoped<QueryService>();
    }
}