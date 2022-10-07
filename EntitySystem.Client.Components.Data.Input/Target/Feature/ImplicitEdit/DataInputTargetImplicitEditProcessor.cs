using EntitySystem.Client.Components.Data.Input.Parameters;
using EntitySystem.Client.Components.Data.Input.Target.Property;

namespace EntitySystem.Client.Components.Data.Input.Target.Feature.ImplicitEdit;

public class DataInputTargetImplicitEditProcessor : IDataInputTargetImplicitEditProcessor
{
    private readonly IDataInputTargetImplicitEditFeature _implicitEditFeature;

    public DataInputTargetImplicitEditProcessor(IDataInputTargetImplicitEditFeature implicitEditFeature)
    {
        _implicitEditFeature = implicitEditFeature;
    }

    public void Process<TParent, TChild>(IDataInputParameters<IDataInputTargetProperty<TParent, TChild>, TParent, TChild> parameters)
    {
        if (!parameters.Property.Features.HasFeature<DataInputTargetEnableImplicitEditFeature>()) return;

        parameters.Features.AddFeature(_implicitEditFeature);
    }
}