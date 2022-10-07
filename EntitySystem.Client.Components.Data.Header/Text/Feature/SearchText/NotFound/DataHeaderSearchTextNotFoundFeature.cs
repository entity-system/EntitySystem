using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Header.Text.Feature.SearchText.NotFound;

public class DataHeaderSearchTextNotFoundFeature : IDataHeaderSearchTextNotFoundFeature
{
    public IEnumerable<IRenderer> Build<TEntity>(BaseDataHeaderSearchText<TEntity> search)
    {
        if (search.List == null || search.List.Any()) yield break;

        yield return new Renderer<BaseDataHeaderSearchText<TEntity>, DataHeaderSearchTextNotFound<TEntity>>(search);
    }
}