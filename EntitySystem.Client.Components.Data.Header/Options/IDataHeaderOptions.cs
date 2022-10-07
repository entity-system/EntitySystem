using System.Threading.Tasks;
using EntitySystem.Client.Abstract.Domain.Feature;

namespace EntitySystem.Client.Components.Data.Header.Options;

public interface IDataHeaderOptions : IFeatured
{
    Task OnRefreshAsync();
}