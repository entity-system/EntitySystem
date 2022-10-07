using System.Threading.Tasks;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Registrations;

namespace EntitySystem.Client.Components.Data.Input.Target.Feature;

public interface IDataInputTargetStartFeature : IFeature, IRegistrable
{
    Task OnParametersSetAsync<TParent, TChild>(BaseDataInputTarget<TParent, TChild> inputTarget);
}