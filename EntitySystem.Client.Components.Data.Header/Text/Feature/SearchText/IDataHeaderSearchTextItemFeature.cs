using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Header.Text.Feature.SearchText;

public interface IDataHeaderSearchTextItemFeature : IRegistrable, IFeature
{
    IEnumerable<IRenderer> Build<TEntity>(BaseDataHeaderSearchText<TEntity> headerSearch);
}