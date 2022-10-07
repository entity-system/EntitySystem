using EntitySystem.Client.Components.Data.Header.Factory;
using EntitySystem.Client.Components.Data.Header.Feature.OrderButton;
using EntitySystem.Client.Components.Data.Header.Text.Factory;
using EntitySystem.Client.Components.Data.Header.Text.Feature.SearchText;
using EntitySystem.Client.Components.Data.Header.Text.Feature.SearchText.FoundItem;
using EntitySystem.Client.Components.Data.Header.Text.Feature.SearchText.NotFound;
using Microsoft.Extensions.DependencyInjection;

namespace EntitySystem.Client.Components.Data.Header;

public static class DataHeaderRegistrations
{
    public static IServiceCollection AddDataHeaderRegistrations(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IDataHeaderFactory, DataHeaderFactory>()
            .AddSingleton<IDataHeaderOrderButtonProcessor, DataHeaderOrderButtonProcessor>()
            .AddSingleton<IDataHeaderOrderButtonFeature, DataHeaderOrderButtonFeature>()
            .AddSingleton<IDataHeaderTextFactory, DataHeaderTextFactory>()
            .AddSingleton<IDataHeaderSearchTextProcessor, DataHeaderSearchTextProcessor>()
            .AddSingleton<IDataHeaderSearchTextFeature, DataHeaderSearchTextFeature>()
            .AddSingleton<IDataHeaderSearchTextNotFoundProcessor, DataHeaderSearchTextNotFoundProcessor>()
            .AddSingleton<IDataHeaderSearchTextNotFoundFeature, DataHeaderSearchTextNotFoundFeature>()
            .AddSingleton<IDataHeaderSearchTextFoundItemProcessor, DataHeaderSearchTextFoundItemProcessor>()
            .AddSingleton<IDataHeaderSearchTextFoundItemFeature, DataHeaderSearchTextFoundItemFeature>();
    }
}