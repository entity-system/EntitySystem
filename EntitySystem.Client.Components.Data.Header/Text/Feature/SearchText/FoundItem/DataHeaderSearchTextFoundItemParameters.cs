using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Parameters;

namespace EntitySystem.Client.Components.Data.Header.Text.Feature.SearchText.FoundItem;

public class DataHeaderSearchTextFoundItemParameters<TEntity> : Featured, IParameters
{
    public BaseDataHeaderSearchText<TEntity> HeaderSearch { get; }

    public TEntity Entity { get; }

    public DataHeaderSearchTextFoundItemParameters(BaseDataHeaderSearchText<TEntity> headerSearch, TEntity entity)
    {
        HeaderSearch = headerSearch;
        Entity = entity;
    }
}