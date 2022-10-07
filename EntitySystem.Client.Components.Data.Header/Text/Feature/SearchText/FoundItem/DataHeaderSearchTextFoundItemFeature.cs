using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Header.Text.Feature.SearchText.FoundItem;

internal class DataHeaderSearchTextFoundItemFeature : IDataHeaderSearchTextFoundItemFeature
{
    public IEnumerable<IRenderer> Build<TEntity>(BaseDataHeaderSearchText<TEntity> headerSearch)
    {
        return (headerSearch.List ?? Enumerable.Empty<TEntity>())
            .Select(entity => new DataHeaderSearchTextFoundItemParameters<TEntity>(headerSearch, entity))
            .Select(parameters => new Renderer<DataHeaderSearchTextFoundItemParameters<TEntity>, DataHeaderSearchTextFoundItem<TEntity>>(parameters, 10));
    }
}