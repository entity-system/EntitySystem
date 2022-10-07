using System.Net.Http;
using System.Threading.Tasks;

namespace EntitySystem.Client.Services;

public interface IExceptionService
{
    Task ProcessAsync(HttpRequestMessage request, HttpResponseMessage response, params int[] validCodes);
}