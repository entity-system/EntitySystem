using System.IO;
using System.Threading.Tasks;

namespace EntitySystem.Client.Abstract.Services;

public interface IDownloadService
{
    Task DownloadStreamAsync(string name, Stream stream);
    string CreateFileName(string extension);
}