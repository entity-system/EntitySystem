namespace EntitySystem.Client.Components.Data.Header.Text.Feature.SearchText.FoundItem;

internal class DataHeaderSearchTextFoundItemProcessor : IDataHeaderSearchTextFoundItemProcessor
{
    private readonly IDataHeaderSearchTextFoundItemFeature _foundItemFeature;

    public DataHeaderSearchTextFoundItemProcessor(IDataHeaderSearchTextFoundItemFeature foundItemFeature)
    {
        _foundItemFeature = foundItemFeature;
    }

    public void Process<TEntity>(DataHeaderSearchTextParameters<TEntity> parameters)
    {
        parameters.Features.AddFeature(_foundItemFeature);
    }
}