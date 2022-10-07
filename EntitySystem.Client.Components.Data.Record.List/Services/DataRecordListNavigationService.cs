using EntitySystem.Client.Abstract.Extensions;
using EntitySystem.Client.Components.Data.Output.Feature.LinkFormatter;
using EntitySystem.Client.Components.Data.Record.List.Source;
using Microsoft.AspNetCore.Components;

namespace EntitySystem.Client.Components.Data.Record.List.Services;

public class DataRecordListNavigationService : IDataRecordListNavigationService
{
    private readonly NavigationManager _navigationManager;

    public DataRecordListNavigationService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    public bool TryNavigateToRecordInfo<TKey>(IDataRecordListSource<TKey> source, TKey key)
    {
        var enableLinkFeature = source.Features.GetFeature<DataOutputEnableLink<TKey>>();

        if (enableLinkFeature is not { Options.IsSelf: true }) return false;

        var relativeLink = enableLinkFeature.Options.LinkFactory(key);

        var absoluteLink = _navigationManager.CreateLink(relativeLink);

        _navigationManager.NavigateTo(absoluteLink);

        return true;
    }
}