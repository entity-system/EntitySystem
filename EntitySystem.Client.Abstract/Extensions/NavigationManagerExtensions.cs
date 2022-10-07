using System;
using Microsoft.AspNetCore.Components;

namespace EntitySystem.Client.Abstract.Extensions;

public static class NavigationManagerExtensions
{
    public static int GetAnchorIndex(this NavigationManager manager)
    {
        var anchor = manager.GetAnchor();

        return int.TryParse(anchor, out var index) ? index : 0;
    }

    public static string GetAnchor(this NavigationManager manager)
    {
        var uri = new Uri(manager.Uri);

        var fragment = uri.Fragment;

        return string.IsNullOrEmpty(fragment) ? fragment : fragment[1..];
    }

    public static string CreateAnchorIndex(this NavigationManager manager, int anchorIndex)
    {
        return manager.CreateAnchor($"{anchorIndex}");
    }

    public static string CreateAnchor(this NavigationManager manager, string anchor)
    {
        var uriBuilder = new UriBuilder(manager.Uri)
        {
            Fragment = $"#{anchor}"
        };

        return uriBuilder.ToString();
    }

    public static string CreateLink(this NavigationManager navigationManager, string link)
    {
        if (!Uri.IsWellFormedUriString(link, UriKind.Relative)) return link;

        var currentUri = new Uri(navigationManager.GetCurrentUri());

        var targetUri = new Uri(currentUri, link);

        return targetUri.ToString();
    }

    private static string GetCurrentUri(this NavigationManager navigationManager)
    {
        var currentUri = navigationManager.Uri;

        var uriBuilder = new UriBuilder(currentUri)
        {
            Query = string.Empty,
            Fragment = string.Empty
        };

        var built = uriBuilder.ToString();

        return built.EndsWith('/') ? built : $"{built}/";
    }
}