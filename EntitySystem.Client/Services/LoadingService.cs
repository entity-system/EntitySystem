using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntitySystem.Shared.Exceptions;

namespace EntitySystem.Client.Services;

public class LoadingService : ILoadingService
{
    private readonly IAlertService _alertService;

    private readonly HashSet<object> _set = new();

    public LoadingService(IAlertService alertService)
    {
        _alertService = alertService;
    }

    public event Action OnChange;

    public async Task<T> LoadAsync<T>(object instance, Func<Task<T>> action)
    {
        StartLoading(instance);

        try
        {
            return await action();
        }
        catch (Exception e) when (e is FriendlyException clientException)
        {
            _alertService.Add(AlertType.Error, clientException.Notification);

            throw;
        }
        catch (Exception e) when (e is ClientRedirectException)
        {
            _alertService.Add(AlertType.Warning, e.Message);

            throw;
        }
        catch (Exception e)
        {
            _alertService.Add(AlertType.Error, e.Message);

            throw;
        }
        finally
        {
            StopLoading(instance);
        }
    }

    public void StartLoading(object instance)
    {
        _set.Add(instance);

        OnChange?.Invoke();
    }

    public void StopLoading(object instance)
    {
        _set.Remove(instance);

        OnChange?.Invoke();
    }

    public bool IsLoading()
    {
        return _set.Any();
    }
}