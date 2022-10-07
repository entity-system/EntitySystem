using System;
using System.Threading.Tasks;

namespace EntitySystem.Client.Services;

public interface ILoadingService
{
    event Action OnChange;
    Task<T> LoadAsync<T>(object instance, Func<Task<T>> action);
    void StartLoading(object instance);
    void StopLoading(object instance);
    bool IsLoading();
}