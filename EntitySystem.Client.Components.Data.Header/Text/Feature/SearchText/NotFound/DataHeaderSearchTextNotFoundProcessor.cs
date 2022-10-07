namespace EntitySystem.Client.Components.Data.Header.Text.Feature.SearchText.NotFound;

public class DataHeaderSearchTextNotFoundProcessor : IDataHeaderSearchTextNotFoundProcessor
{
    private readonly IDataHeaderSearchTextNotFoundFeature _notFoundFeature;

    public DataHeaderSearchTextNotFoundProcessor(IDataHeaderSearchTextNotFoundFeature notFoundFeature)
    {
        _notFoundFeature = notFoundFeature;
    }

    public void Process<TEntity>(DataHeaderSearchTextParameters<TEntity> parameters)
    {
        parameters.Features.AddFeature(_notFoundFeature);
    }
}