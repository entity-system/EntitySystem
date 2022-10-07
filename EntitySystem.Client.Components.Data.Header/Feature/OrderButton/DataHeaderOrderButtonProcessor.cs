using EntitySystem.Client.Components.Data.Header.Parameters;
using EntitySystem.Client.Components.Data.Header.Property;

namespace EntitySystem.Client.Components.Data.Header.Feature.OrderButton;

public class DataHeaderOrderButtonProcessor : IDataHeaderOrderButtonProcessor
{
    private readonly IDataHeaderOrderButtonFeature _orderButtonFeature;

    public DataHeaderOrderButtonProcessor(IDataHeaderOrderButtonFeature orderButtonFeature)
    {
        _orderButtonFeature = orderButtonFeature;
    }

    public void Process<TProperty, TEntity, TValue>(IDataHeaderParameters<TProperty, TEntity, TValue> parameters) where TProperty : IDataHeaderProperty<TEntity, TValue>
    {
        if (parameters.Property.Features.HasFeature<DataHeaderDisableOrderFeature>()) return;

        parameters.Features.AddFeature(_orderButtonFeature);
    }
}