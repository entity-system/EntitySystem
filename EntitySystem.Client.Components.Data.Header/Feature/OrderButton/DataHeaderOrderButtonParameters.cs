using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Parameters;
using EntitySystem.Client.Components.Data.Header.Property;

namespace EntitySystem.Client.Components.Data.Header.Feature.OrderButton;

public class DataHeaderOrderButtonParameters<TProperty, TEntity, TValue> : Featured, IParameters
    where TProperty : IDataHeaderProperty<TEntity, TValue>
{
    public BaseDataHeader<TProperty, TEntity, TValue> DataHeader { get; }

    public bool Descending { get; }

    public string Text { get; }

    public DataHeaderOrderButtonParameters(BaseDataHeader<TProperty, TEntity, TValue> dataHeader, bool descending, string text)
    {
        DataHeader = dataHeader;
        Descending = descending;
        Text = text;
    }
}