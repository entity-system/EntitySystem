using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Header.Property;

namespace EntitySystem.Client.Components.Data.Header.Feature;

public interface IDataHeaderItemFeature : IFeature, IRegistrable
{
    IEnumerable<IRenderer> Build<TProperty, TEntity, TValue>(BaseDataHeader<TProperty, TEntity, TValue> header)
        where TProperty : IDataHeaderProperty<TEntity, TValue>;
}