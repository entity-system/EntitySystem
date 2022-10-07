using System.Threading.Tasks;
using EntitySystem.Client.Components.Data.Input.Target.Feature.CreateItem;

namespace EntitySystem.Client.Components.Data.Input.Target.Feature.ImplicitCreate;

public class DataInputTargetImplicitCreateFeature : IDataInputTargetImplicitCreateFeature
{
    public async Task OnParametersSetAsync<TParent, TChild>(BaseDataInputTarget<TParent, TChild> inputTarget)
    {
        await inputTarget.OnCreateAsync();
    }
}