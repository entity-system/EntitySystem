using System.Threading.Tasks;

namespace EntitySystem.Server.Services
{
    public interface IFinalService
    {
        Task ProcessAsync(bool success);
    }
}
