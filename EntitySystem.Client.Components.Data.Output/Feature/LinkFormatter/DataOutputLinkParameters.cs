using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Parameters;

namespace EntitySystem.Client.Components.Data.Output.Feature.LinkFormatter;

public class DataOutputLinkParameters<TKey, TEntity, TValue> : Featured, IParameters
{
    public DataOutputLinkOptions<TKey> Options { get; }

    public BaseDataOutput<TKey, TEntity, TValue> Output { get; }

    public DataOutputLinkParameters(BaseDataOutput<TKey, TEntity, TValue> output)
    {
        Options = output.Parameters.Features.GetFeature<DataOutputLinkOptions<TKey>>();
        Output = output;
    }

    public string Link => Options.LinkFactory(Output.Record.Key);

    public string Target => Options.LinkTarget;
}