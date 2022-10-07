using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Header.Text.Property;

namespace EntitySystem.Client.Components.Data.Header.Text.Feature;

public interface IDataHeaderTextItemFeature : IRegistrable, IFeature
{
    IEnumerable<IRenderer> Build<TEntity>(BaseDataHeader<IDataHeaderTextProperty<TEntity>, TEntity, string> header);
}