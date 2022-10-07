using System.Threading.Tasks;

namespace EntitySystem.Client.Components.Data.Input.Options;

public interface IDataInputOptions
{
    public bool ReadOnly { get; }

    public bool IsNewEntity { get; }

    Task OnChangeAsync();
}