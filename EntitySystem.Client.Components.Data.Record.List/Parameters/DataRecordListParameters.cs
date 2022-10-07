using EntitySystem.Client.Abstract.Components;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Record.List.Options;

namespace EntitySystem.Client.Components.Data.Record.List.Parameters;

public class DataRecordListParameters<TKey> : Featured, IDataRecordListParameters<TKey>
{
    public IDataRecordListOptions<TKey> Options { get; }

    public DataRecordListParameters(IDataRecordListOptions<TKey> options)
    {
        Options = options;
    }

    public IRenderer Build<TComponent>() where TComponent : BaseRendered<IDataRecordListParameters<TKey>>
    {
        return new Renderer<IDataRecordListParameters<TKey>, TComponent>(this);
    }
}