using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Abstract.Services;
using EntitySystem.Client.Components.Data.Entity.Dialog;
using EntitySystem.Client.Components.Data.Header;
using EntitySystem.Client.Components.Data.Input;
using EntitySystem.Client.Components.Data.Output;
using EntitySystem.Client.Components.Data.Record.Info;
using EntitySystem.Client.Components.Data.Record.List;
using EntitySystem.Client.Components.Data.Table;
using EntitySystem.Client.Components.Data.Table.Join;
using EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin;
using EntitySystem.Client.Domain.Data.Join.Factory;
using EntitySystem.Client.Domain.Data.Property.Factory;
using EntitySystem.Client.Domain.Data.Source.Factory;
using EntitySystem.Client.Domain.Data.Target.Factory;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Abstract.Providers;
using EntitySystem.Shared.Abstract.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EntitySystem.Client;

public static class EntitySystemClientRegistrations
{
    public static IServiceCollection AddEntitySystemClient(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddClientServices()
            .AddDataSourceRegistrations()
            .AddDataPropertyRegistrations()
            .AddDataJoinRegistrations()
            .AddReverseCloneJoinRegistrations();
    }

    private static IServiceCollection AddClientServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IExceptionService, ExceptionService>()
            .AddSingleton<IDownloadService, DownloadService>()
            .AddSingleton<IDescriptorProvider>(new DescriptorProvider(serviceCollection))
            .AddSingleton<IServiceFinder, ServiceFinder>()
            .AddSingleton<IRegistrationProvider, RegistrationProvider>()
            .AddSingleton<IPersistenceService, PersistenceService>()
            .AddSingleton<ITimeService, TimeService>()
            .AddSingleton<IRedirectService, RedirectService>()
            .AddSingleton<ILoadingService, LoadingService>()
            .AddSingleton<IAlertService, AlertService>()
            .AddSingleton<IDataService, DataService>();
    }

    private static IServiceCollection AddDataSourceRegistrations(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IDataSourceFactory, DataSourceFactory>()
            .AddDataTableRegistrations()
            .AddDataRecordListRegistrations()
            .AddDataRecordInfoRegistrations()
            .AddEntityDialogRegistrations();
    }

    private static IServiceCollection AddDataPropertyRegistrations(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IDataPropertyFactory, DataPropertyFactory>()
            .AddSingleton<IDataTargetFactory, DataTargetFactory>()
            .AddDataHeaderRegistrations()
            .AddDataInputRegistrations()
            .AddDataOutputRegistrations();
    }

    private static IServiceCollection AddDataJoinRegistrations(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IDataJoinFactory, DataJoinFactory>()
            .AddDataTableJoinRegistrations();
    }
}