using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace EntitySystem.Client.Services;

public class PersistenceService : IPersistenceService
{
    private readonly IJSRuntime _jSRuntime;

    public PersistenceService(IJSRuntime jSRuntime)
    {
        _jSRuntime = jSRuntime;
    }

    public async Task SaveAsync<T>(T entity)
        where T : class
    {
        await _jSRuntime.InvokeVoidAsync("localStorage.setItem", typeof(T).FullName ?? throw new InvalidOperationException(), JsonSerializer.Serialize(entity));
    }

    public async Task<T> GetAsync<T>()
    {
        var result = await _jSRuntime.InvokeAsync<string>("localStorage.getItem", typeof(T).FullName ?? throw new InvalidOperationException());

        return result == null ? default : JsonSerializer.Deserialize<T>(result);
    }

    public async Task DeleteAsync<T>(T _)
    {
        if (Equals(await GetAsync<T>(), default(T))) return;

        await _jSRuntime.InvokeAsync<string>("localStorage.removeItem", typeof(T).FullName ?? throw new InvalidOperationException());
    }
}