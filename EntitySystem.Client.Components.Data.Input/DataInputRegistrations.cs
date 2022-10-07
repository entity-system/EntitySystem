using EntitySystem.Client.Components.Data.Input.Date.Factory;
using EntitySystem.Client.Components.Data.Input.Decimal.Factory;
using EntitySystem.Client.Components.Data.Input.Factory;
using EntitySystem.Client.Components.Data.Input.Guid.Factory;
using EntitySystem.Client.Components.Data.Input.Integer.Factory;
using EntitySystem.Client.Components.Data.Input.Long.Factory;
using EntitySystem.Client.Components.Data.Input.Target.Factory;
using EntitySystem.Client.Components.Data.Input.Target.Feature.CreateItem;
using EntitySystem.Client.Components.Data.Input.Target.Feature.FoundItem;
using EntitySystem.Client.Components.Data.Input.Target.Feature.ImplicitCreate;
using EntitySystem.Client.Components.Data.Input.Target.Feature.ImplicitEdit;
using EntitySystem.Client.Components.Data.Input.Target.Feature.NotFound;
using EntitySystem.Client.Components.Data.Input.Text.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace EntitySystem.Client.Components.Data.Input;

public static class DataInputRegistrations
{
    public static IServiceCollection AddDataInputRegistrations(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IDataInputFactory, DataInputFactory>()
            .AddSingleton<IDataInputDateFactory, DataInputDateFactory>()
            .AddSingleton<IDataInputGuidFactory, DataInputGuidFactory>()
            .AddSingleton<IDataInputIntegerFactory, DataInputIntegerFactory>()
            .AddSingleton<IDataInputDecimalFactory, DataInputDecimalFactory>()
            .AddSingleton<IDataInputLongFactory, DataInputLongFactory>()
            .AddSingleton<IDataInputTextFactory, DataInputTextFactory>()
            .AddSingleton<IDataInputTargetFactory, DataInputTargetFactory>()
            .AddSingleton<IDataInputTargetCreateItemProcessor, DataInputTargetCreateItemProcessor>()
            .AddSingleton<IDataInputTargetCreateItemFeature, DataInputTargetCreateItemFeature>()
            .AddSingleton<IDataInputTargetFoundItemProcessor, DataInputTargetFoundItemProcessor>()
            .AddSingleton<IDataInputTargetFoundItemFeature, DataInputTargetFoundItemFeature>()
            .AddSingleton<IDataInputTargetImplicitCreateProcessor, DataInputTargetImplicitCreateProcessor>()
            .AddSingleton<IDataInputTargetImplicitCreateFeature, DataInputTargetImplicitCreateFeature>()
            .AddSingleton<IDataInputTargetImplicitEditProcessor, DataInputTargetImplicitEditProcessor>()
            .AddSingleton<IDataInputTargetImplicitEditFeature, DataInputTargetImplicitEditFeature>()
            .AddSingleton<IDataInputTargetNotFoundProcessor, DataInputTargetNotFoundProcessor>()
            .AddSingleton<IDataInputTargetNotFoundFeature, DataInputTargetNotFoundFeature>();
    }
}