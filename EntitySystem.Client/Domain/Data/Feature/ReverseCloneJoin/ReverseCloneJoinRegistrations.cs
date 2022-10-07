using EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Join;
using EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Source;
using EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Target;
using Microsoft.Extensions.DependencyInjection;

namespace EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin;

public static class ReverseCloneJoinRegistrations
{
    public static IServiceCollection AddReverseCloneJoinRegistrations(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IDataSourceReverseCloneJoinFeatureProcessor, DataSourceReverseCloneJoinFeatureProcessor>()
            .AddSingleton<IDataTargetReverseCloneJoinFeatureProcessor, DataTargetReverseCloneJoinFeatureProcessor>()
            .AddSingleton<IDataJoinReverseCloneJoinFeatureProcessor, DataJoinReverseCloneJoinFeatureProcessor>();
    }
}