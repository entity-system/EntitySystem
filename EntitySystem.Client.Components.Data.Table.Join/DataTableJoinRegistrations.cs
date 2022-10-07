using EntitySystem.Client.Components.Data.Table.Join.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace EntitySystem.Client.Components.Data.Table.Join;

public static class DataTableJoinRegistrations
{
    public static IServiceCollection AddDataTableJoinRegistrations(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IDataTableJoinFactory, DataTableJoinFactory>();
    }
}