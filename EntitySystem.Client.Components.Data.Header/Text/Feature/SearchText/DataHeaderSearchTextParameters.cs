using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Parameters;
using EntitySystem.Client.Components.Data.Header.Text.Property;

namespace EntitySystem.Client.Components.Data.Header.Text.Feature.SearchText;

public class DataHeaderSearchTextParameters<TEntity> : Featured, IParameters
{
    public BaseDataHeader<IDataHeaderTextProperty<TEntity>, TEntity, string> HeaderText { get; }

    public DataHeaderSearchTextParameters(BaseDataHeader<IDataHeaderTextProperty<TEntity>, TEntity, string> headerText)
    {
        HeaderText = headerText;
    }
}