using System.IO;
using System.Threading.Tasks;
using EntitySystem.Shared.Abstract.Services;
using Microsoft.JSInterop;

namespace EntitySystem.Client.Abstract.Services;

    public class DownloadService : IDownloadService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly ITimeService _timeService;

        public DownloadService(IJSRuntime jsRuntime, ITimeService timeService)
        {
            _jsRuntime = jsRuntime;
            _timeService = timeService;
        }

        public string CreateFileName(string extension)
        {
            var now = _timeService.GetTimeNow();

            return $"{now:yyyyMMddTHHmmss}{extension}";
        }

        public async Task DownloadStreamAsync(string name, Stream stream)
        {
            using var reference = new DotNetStreamReference(stream);

            await _jsRuntime.InvokeVoidAsync("downloadFileFromStream", name, reference);
        }
    }
