using EntitySystem.Client.Components.Data.Table.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace EntitySystem.Client.Components.Data.Table;

public static class DataTableRegistrations
{
    public static IServiceCollection AddDataTableRegistrations(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IDataTableFactory, DataTableFactory>();
    }
}