using EntitySystem.Client.Components.Data.Input.Parameters;
using EntitySystem.Client.Components.Data.Input.Target.Property;

namespace EntitySystem.Client.Components.Data.Input.Target.Feature.ImplicitCreate;

public class DataInputTargetImplicitCreateProcessor : IDataInputTargetImplicitCreateProcessor
{
    private readonly IDataInputTargetImplicitCreateFeature _implicitCreateFeature;

    public DataInputTargetImplicitCreateProcessor(IDataInputTargetImplicitCreateFeature implicitCreateFeature)
    {
        _implicitCreateFeature = implicitCreateFeature;
    }

    public void Process<TParent, TChild>(IDataInputParameters<IDataInputTargetProperty<TParent, TChild>, TParent, TChild> parameters)
    {
        if (!parameters.Property.Features.HasFeature<DataInputTargetEnableImplicitCreateFeature>()) return;

        parameters.Features.AddFeature(_implicitCreateFeature);
    }
}