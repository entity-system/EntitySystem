using System.Threading.Tasks;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Registrations;

namespace EntitySystem.Client.Components.Data.Entity.Dialog.Feature;

public interface IDataEntityDialogStartFeature : IFeature, IRegistrable
{
    public Task OnParametersSetAsync<TEntity>(BaseDataEntityDialog<TEntity> dialog);
}