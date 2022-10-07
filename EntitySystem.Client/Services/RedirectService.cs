using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EntitySystem.Client.Services;

public class RedirectService : IRedirectService
{
    private readonly NavigationManager _navigationManager;
    private readonly IJSRuntime _jsRuntime;

    public RedirectService(NavigationManager navigationManager, IJSRuntime jsRuntime)
    {
        _navigationManager = navigationManager;
        _jsRuntime = jsRuntime;
    }

    public async Task RedirectToFromOrBackAsync()
    {
        if (GetFromParameter() is { } fromParameter) await RedirectToAsync(fromParameter);

        else await RedirectBackAsync();

    }

    public async Task RedirectToFromOrIndexAsync()
    {
        if (GetFromParameter() is { } fromParameter) await RedirectToAsync(fromParameter);

        else await RedirectToIndexAsync();
    }

    public async Task RedirectToAndKeepOldFromAsync(string to)
    {
        var (uriBuilderTo, queryTo) = GetUriBuilderAndQuery(to);

        if (GetFromParameter() is { } fromParameter)
        {
            SetFromParameter(queryTo, fromParameter);

            SetQuery(uriBuilderTo, queryTo);
        }

        await RedirectToAsync(uriBuilderTo);
    }

    public async Task RedirectToAndSetFromIndexAsync(string to)
    {
        var (uriBuilderTo, queryTo) = GetUriBuilderAndQuery(to);

        SetFromParameter(queryTo, "/");

        SetQuery(uriBuilderTo, queryTo);

        await RedirectToAsync(uriBuilderTo);
    }

    public async Task RedirectToAndIncludeFromAsync(string to)
    {
        var (uriBuilderFrom, queryFrom) = GetCurrentUriBuilderAndQuery();

        var (uriBuilderTo, queryTo) = GetUriBuilderAndQuery(to);

        if (GetFromParameter(queryFrom) is { })
        {
            RemoveFromParameter(queryFrom);

            SetQuery(uriBuilderFrom, queryFrom);
        }

        SetFromParameter(queryTo, uriBuilderFrom);

        SetQuery(uriBuilderTo, queryTo);

        await RedirectToAsync(uriBuilderTo);
    }

    public async Task RedirectToIndexAsync()
    {
        await RedirectToAsync("/");
    }

    public async Task RedirectToAsync(UriBuilder uriBuilder)
    {
        var uri = uriBuilder.ToString();

        await RedirectToAsync(uri);
    }

    public async Task RedirectToAsync(string uri)
    {
        _navigationManager.NavigateTo(uri);

        await Task.CompletedTask;
    }

    public async Task RedirectBackAsync()
    {
        await _jsRuntime.InvokeVoidAsync("window.history.back");
    }

    private (UriBuilder uriBuilder, NameValueCollection queryParameters) GetUriBuilderAndQuery(string to)
    {
        var absoluteTo = _navigationManager.ToAbsoluteUri(to ?? "/");

        var uriBuilderTo = new UriBuilder(absoluteTo);

        var queryTo = HttpUtility.ParseQueryString(uriBuilderTo.Query);

        return (uriBuilderTo, queryTo);
    }

    private string GetFromParameter()
    {
        var currentQuery = GetCurrentQuery();

        return GetFromParameter(currentQuery);
    }

    private static string GetFromParameter(NameValueCollection query)
    {
        return query["from"];
    }

    private static void SetFromParameter(NameValueCollection query, UriBuilder uriBuilder)
    {
        query["from"] = uriBuilder.ToString();
    }

    private static void SetFromParameter(NameValueCollection query, string parameter)
    {
        query["from"] = parameter;
    }

    private static void RemoveFromParameter(NameValueCollection query)
    {
        query.Remove("from");
    }

    private static void SetQuery(UriBuilder uriBuilder, NameValueCollection query)
    {
        uriBuilder.Query = $"{query}";
    }

    private NameValueCollection GetCurrentQuery()
    {
        var (_, query) = GetCurrentUriBuilderAndQuery();

        return query;
    }

    private (UriBuilder uriBuilder, NameValueCollection query) GetCurrentUriBuilderAndQuery()
    {
        var uriBuilder = new UriBuilder(_navigationManager.Uri);

        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        return (uriBuilder, query);
    }

    /*public async Task RedirectAsync(string to = null, bool toFrom = false, bool back = false, bool includeFrom = false, bool keepOldFrom = false)
    {
        var absoluteTo = _navigationManager.ToAbsoluteUri(to ?? "/");

        var uriBuilderTo = new UriBuilder(absoluteTo);

        var queryTo = HttpUtility.ParseQueryString(uriBuilderTo.Query);

        var uriBuilderFrom = new UriBuilder(_navigationManager.Uri);

        var queryFrom = HttpUtility.ParseQueryString(uriBuilderFrom.Query);

        if (back && queryFrom["from"] == null)
        {
            await _jsRuntime.InvokeVoidAsync("window.history.back");

            return;
        }

        if (back || toFrom)
        {
            _navigationManager.NavigateTo(queryFrom["from"] ?? "/");

            return;
        }

        if (keepOldFrom && queryFrom["from"] != null)
        {
            queryTo["from"] = queryFrom["from"];

            uriBuilderTo.Query = $"{queryTo}";
        }

        if (includeFrom)
        {
            queryFrom.Remove("from");

            uriBuilderFrom.Query = $"{queryFrom}";

            queryTo["from"] = uriBuilderFrom.ToString();

            uriBuilderTo.Query = $"{queryTo}";
        }

        var redirectTo = uriBuilderTo.ToString();

        _navigationManager.NavigateTo(redirectTo);
    }*/
}