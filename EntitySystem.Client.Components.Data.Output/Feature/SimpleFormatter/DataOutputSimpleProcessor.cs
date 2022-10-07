using EntitySystem.Client.Components.Data.Output.Parameters;

namespace EntitySystem.Client.Components.Data.Output.Feature.SimpleFormatter;

public class DataOutputSimpleProcessor : IDataOutputSimpleProcessor
{
    private readonly IDataOutputSimpleFormatter _simpleFormatter;

    public DataOutputSimpleProcessor(IDataOutputSimpleFormatter simpleFormatter)
    {
        _simpleFormatter = simpleFormatter;
    }

    public void Process<TKey, TEntity, TValue>(DataOutputParameters<TKey, TEntity, TValue> parameters)
    {
        parameters.Features.AddFeature(_simpleFormatter);
    }
}