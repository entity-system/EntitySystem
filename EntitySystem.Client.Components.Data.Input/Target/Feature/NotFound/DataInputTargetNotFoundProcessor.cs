using EntitySystem.Client.Components.Data.Input.Parameters;
using EntitySystem.Client.Components.Data.Input.Target.Property;

namespace EntitySystem.Client.Components.Data.Input.Target.Feature.NotFound;

public class DataInputTargetNotFoundProcessor : IDataInputTargetNotFoundProcessor
{
    private readonly IDataInputTargetNotFoundFeature _notFoundFeature;

    public DataInputTargetNotFoundProcessor(IDataInputTargetNotFoundFeature notFoundFeature)
    {
        _notFoundFeature = notFoundFeature;
    }

    public void Process<TParent, TChild>(IDataInputParameters<IDataInputTargetProperty<TParent, TChild>, TParent, TChild> parameters)
    {
        parameters.Features.AddFeature(_notFoundFeature);
    }
}