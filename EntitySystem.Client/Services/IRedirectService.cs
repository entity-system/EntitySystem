using System;
using System.Threading.Tasks;

namespace EntitySystem.Client.Services;

public interface IRedirectService
{
    Task RedirectToFromOrBackAsync();
    Task RedirectToFromOrIndexAsync();
    Task RedirectToAndKeepOldFromAsync(string to);
    Task RedirectToAndSetFromIndexAsync(string to);
    Task RedirectToAndIncludeFromAsync(string to);
    Task RedirectToIndexAsync();
    Task RedirectToAsync(UriBuilder uriBuilder);
    Task RedirectToAsync(string uri);
    Task RedirectBackAsync();
}