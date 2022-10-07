using EntitySystem.Client.Abstract.Components;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Record.Info.Options;

namespace EntitySystem.Client.Components.Data.Record.Info.Parameters;

public class DataRecordInfoParameters<TKey> : Featured, IDataRecordInfoParameters<TKey>
{
    public IDataRecordInfoOptions<TKey> Options { get; }

    public DataRecordInfoParameters(IDataRecordInfoOptions<TKey> options)
    {
        Options = options;
    }

    public IRenderer Build<TComponent>() where TComponent : BaseRendered<IDataRecordInfoParameters<TKey>>
    {
        return new Renderer<IDataRecordInfoParameters<TKey>, TComponent>(this);
    }
}