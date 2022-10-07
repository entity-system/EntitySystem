using EntitySystem.Client.Components.Data.Entity.Dialog.Factory;
using EntitySystem.Client.Components.Data.Entity.Dialog.Feature.HidePropertyInAdd;
using EntitySystem.Client.Components.Data.Entity.Dialog.Feature.HidePropertyInEdit;
using EntitySystem.Client.Components.Data.Entity.Dialog.Feature.InstantAdd;
using Microsoft.Extensions.DependencyInjection;

namespace EntitySystem.Client.Components.Data.Entity.Dialog;

public static class DataEntityDialogRegistrations
{
    public static IServiceCollection AddEntityDialogRegistrations(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IDataEntityDialogFactory, DataEntityDialogFactory>()
            .AddSingleton<IDataEntityDialogInstantAddProcessor, DataEntityDialogInstantAddProcessor>()
            .AddSingleton<IDataEntityDialogInstantAddFeature, DataEntityDialogInstantAddFeature>()
            .AddSingleton<IDataEntityDialogHidePropertyInAddFeature, DataEntityDialogHidePropertyInAddFeature>()
            .AddSingleton<IDataEntityDialogHidePropertyInEditFeature, DataEntityDialogHidePropertyInEditFeature>();
    }
}