using System.Threading.Tasks;

namespace EntitySystem.Client.Components.Data.Entity.Dialog.Feature.InstantAdd;

public class DataEntityDialogInstantAddFeature : IDataEntityDialogInstantAddFeature
{
    public async Task OnParametersSetAsync<TEntity>(BaseDataEntityDialog<TEntity> dialog)
    {
        await dialog.OnSaveOrUpdateAsync();
    }
}