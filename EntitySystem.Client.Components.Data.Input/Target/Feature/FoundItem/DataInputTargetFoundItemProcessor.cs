using EntitySystem.Client.Components.Data.Input.Parameters;
using EntitySystem.Client.Components.Data.Input.Target.Property;

namespace EntitySystem.Client.Components.Data.Input.Target.Feature.FoundItem;

internal class DataInputTargetFoundItemProcessor : IDataInputTargetFoundItemProcessor
{
    private readonly IDataInputTargetFoundItemFeature _foundItemFeature;

    public DataInputTargetFoundItemProcessor(IDataInputTargetFoundItemFeature foundItemFeature)
    {
        _foundItemFeature = foundItemFeature;
    }

    public void Process<TParent, TChild>(IDataInputParameters<IDataInputTargetProperty<TParent, TChild>, TParent, TChild> parameters)
    {
        parameters.Features.AddFeature(_foundItemFeature);
    }
}