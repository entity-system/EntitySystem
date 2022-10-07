using EntitySystem.Client.Components.Data.Output.Parameters;

namespace EntitySystem.Client.Components.Data.Output.Feature.LinkFormatter;

public class DataOutputLinkProcessor : IDataOutputLinkProcessor
{
    private readonly IDataOutputLinkFormatter _outputLinkFormatter;

    public DataOutputLinkProcessor(IDataOutputLinkFormatter outputLinkFormatter)
    {
        _outputLinkFormatter = outputLinkFormatter;
    }

    public void Process<TKey, TEntity, TValue>(DataOutputParameters<TKey, TEntity, TValue> parameters)
    {
        if (parameters.Value.GetOutputObject().GetOutputRecord().GetOutputSource().Features.GetFeature<DataOutputEnableLink<TKey>>() is not { } enableLink) return;

        parameters.Features.AddFeature(enableLink.Options);

        parameters.Features.AddFeature(_outputLinkFormatter);
    }
}