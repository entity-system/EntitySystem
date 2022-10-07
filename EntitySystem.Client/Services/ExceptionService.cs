using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EntitySystem.Client.Abstract.Extensions;
using EntitySystem.Shared.Exceptions;
using EntitySystem.Shared.Extensions;
using EntitySystem.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EntitySystem.Client.Services
{
    public class ExceptionService : IExceptionService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;

        public ExceptionService(IJSRuntime jsRuntime, NavigationManager navigationManager)
        {
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
        }

        public async Task ProcessAsync(HttpRequestMessage request, HttpResponseMessage response, params int[] validCodes)
        {
            if (response.StatusCode.IsClientRedirect())
                throw await CreateClientRedirectExceptionAsync(response);

            if (response.IsSuccessStatusCode || validCodes.Contains((int)response.StatusCode))
                return;

            if (response.StatusCode.IsFriendlyException() && await CreateFriendlyExceptionAsync(response, request) is { } friendlyException)
                throw friendlyException;

            throw await CreateGeneralFriendlyException(response, request);
        }

        private async Task<ClientRedirectException> CreateClientRedirectExceptionAsync(HttpResponseMessage response)
        {
            var redirect = await response.Content.ReadFromJsonAsync<ClientRedirectModel>() ?? throw new InvalidOperationException("Unable to deserialize redirect model.");

            if (redirect.ClientSide) _navigationManager.NavigateTo(_navigationManager.CreateLink(redirect.Uri));

            else await _jsRuntime.InvokeVoidAsync("eval", $"let _discard_ = open(`{redirect.Uri}`, `_blank`)");

            throw new ClientRedirectException(redirect);
        }

        private static async Task<FriendlyException> CreateFriendlyExceptionAsync(HttpResponseMessage response, HttpRequestMessage request)
        {
            var model = await response.Content.ReadFromJsonAsync<FriendlyExceptionModel>();

            if (model == null) return null;

            model.Message = GetInvalidRequestExceptionMessage(response, request, model.Message);

            return new FriendlyException(model);
        }

        private static async Task<GeneralFriendlyException> CreateGeneralFriendlyException(HttpResponseMessage response, HttpRequestMessage request)
        {
            var message = await response.Content.ReadAsStringAsync();

            message = GetInvalidRequestExceptionMessage(response, request, message);

            return new GeneralFriendlyException(message);
        }

        private static string GetInvalidRequestExceptionMessage(HttpResponseMessage response, HttpRequestMessage request, string content)
        {
            return $"Invalid request ({response.StatusCode}) to {request.RequestUri}{(!string.IsNullOrEmpty(content) ? $" {content}" : null)}";
        }
    }
}
