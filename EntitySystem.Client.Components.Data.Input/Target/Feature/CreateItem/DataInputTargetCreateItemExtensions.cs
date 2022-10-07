using System.Threading.Tasks;

namespace EntitySystem.Client.Components.Data.Input.Target.Feature.CreateItem;

public static class DataInputTargetCreateItemExtensions
{
    public static async Task OnCreateAsync<TParent, TChild>(this BaseDataInputTarget<TParent, TChild> inputTarget)
    {
        inputTarget.Text = null;

        var @new = await inputTarget.Property.CreateChildAsync(inputTarget.Entity);

        await inputTarget.FinishAsync(@new);
    }
}