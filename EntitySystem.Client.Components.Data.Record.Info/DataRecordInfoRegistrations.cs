using EntitySystem.Client.Components.Data.Record.Info.Factory;
using EntitySystem.Client.Components.Data.Record.Info.Feature.BackButton;
using EntitySystem.Client.Components.Data.Record.Info.Feature.DeleteButton;
using EntitySystem.Client.Components.Data.Record.Info.Feature.EditButton;
using EntitySystem.Client.Components.Data.Record.Info.Feature.JoinTab;
using EntitySystem.Client.Components.Data.Record.Info.Feature.NoJoin;
using Microsoft.Extensions.DependencyInjection;

namespace EntitySystem.Client.Components.Data.Record.Info;

public static class DataRecordInfoRegistrations
{
    public static IServiceCollection AddDataRecordInfoRegistrations(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IDataRecordInfoFactory, DataRecordInfoFactory>()
            .AddSingleton<IDataRecordInfoBackButtonProcessor, DataRecordInfoBackButtonProcessor>()
            .AddSingleton<IDataRecordInfoBackButtonFeature, DataRecordInfoBackButtonFeature>()
            .AddSingleton<IDataRecordInfoDeleteButtonProcessor, DataRecordInfoDeleteButtonProcessor>()
            .AddSingleton<IDataRecordInfoDeleteButtonFeature, DataRecordInfoDeleteButtonFeature>()
            .AddSingleton<IDataRecordInfoEditButtonProcessor, DataRecordInfoEditButtonProcessor>()
            .AddSingleton<IDataRecordInfoEditButtonFeature, DataRecordInfoEditButtonFeature>()
            .AddSingleton<IDataRecordInfoJoinTabProcessor, DataRecordInfoJoinTabProcessor>()
            .AddSingleton<IDataRecordInfoJoinTabFeature, DataRecordInfoJoinTabFeature>()
            .AddSingleton<IDataRecordInfoNoJoinProcessor, DataRecordInfoNoJoinProcessor>()
            .AddSingleton<IDataRecordInfoNoJoinFeature, DataRecordInfoNoJoinFeature>();
    }
}