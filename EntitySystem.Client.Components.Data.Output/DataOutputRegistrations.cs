using EntitySystem.Client.Components.Data.Output.Factory;
using EntitySystem.Client.Components.Data.Output.Feature.LinkFormatter;
using EntitySystem.Client.Components.Data.Output.Feature.SimpleFormatter;
using Microsoft.Extensions.DependencyInjection;

namespace EntitySystem.Client.Components.Data.Output;

public static class DataOutputRegistrations
{
    public static IServiceCollection AddDataOutputRegistrations(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IDataOutputFactory, DataOutputFactory>()
            .AddSingleton<IDataOutputSimpleProcessor, DataOutputSimpleProcessor>()
            .AddSingleton<IDataOutputSimpleFormatter, DataOutputSimpleFormatter>()
            .AddSingleton<IDataOutputLinkProcessor, DataOutputLinkProcessor>()
            .AddSingleton<IDataOutputLinkFormatter, DataOutputLinkFormatter>();
    }
}