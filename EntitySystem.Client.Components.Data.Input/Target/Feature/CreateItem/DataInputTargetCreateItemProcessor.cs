using EntitySystem.Client.Components.Data.Input.Parameters;
using EntitySystem.Client.Components.Data.Input.Target.Property;

namespace EntitySystem.Client.Components.Data.Input.Target.Feature.CreateItem;

public class DataInputTargetCreateItemProcessor : IDataInputTargetCreateItemProcessor
{
    private readonly IDataInputTargetCreateItemFeature _createItemFeature;

    public DataInputTargetCreateItemProcessor(IDataInputTargetCreateItemFeature createItemFeature)
    {
        _createItemFeature = createItemFeature;
    }

    public void Process<TParent, TChild>(IDataInputParameters<IDataInputTargetProperty<TParent, TChild>, TParent, TChild> parameters)
    {
        if (parameters.Property.Features.HasFeature<DataInputTargetDisableCreateFeature>()) return;

        parameters.Features.AddFeature(_createItemFeature);
    }
}