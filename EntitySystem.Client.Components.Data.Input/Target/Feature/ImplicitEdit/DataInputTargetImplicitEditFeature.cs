using System.Threading.Tasks;

namespace EntitySystem.Client.Components.Data.Input.Target.Feature.ImplicitEdit;

public class DataInputTargetImplicitEditFeature : IDataInputTargetImplicitEditFeature
{
    public async Task OnParametersSetAsync<TParent, TChild>(BaseDataInputTarget<TParent, TChild> inputTarget)
    {
        if (inputTarget.Options.IsNewEntity) return;

        await inputTarget.Property.EditChildAsync(inputTarget.Entity);

        await inputTarget.Options.OnChangeAsync();
    }
}