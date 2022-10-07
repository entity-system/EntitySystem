using EntitySystem.Client.Components.Data.Header.Parameters;
using EntitySystem.Client.Components.Data.Header.Text.Property;

namespace EntitySystem.Client.Components.Data.Header.Text.Feature.SearchText;

public class DataHeaderSearchTextProcessor : IDataHeaderSearchTextProcessor
{
    private readonly IDataHeaderSearchTextFeature _searchTextFeature;

    public DataHeaderSearchTextProcessor(IDataHeaderSearchTextFeature searchTextFeature)
    {
        _searchTextFeature = searchTextFeature;
    }

    public void Process<TEntity>(IDataHeaderParameters<IDataHeaderTextProperty<TEntity>, TEntity, string> parameters)
    {
        if (parameters.Property.Features.HasFeature<DataHeaderDisableSearchText>()) return;

        parameters.Features.AddFeature(_searchTextFeature);
    }
}