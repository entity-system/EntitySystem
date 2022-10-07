using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Parameters;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Header.Options;
using EntitySystem.Client.Components.Data.Header.Origin;
using EntitySystem.Client.Components.Data.Header.Property;

namespace EntitySystem.Client.Components.Data.Header.Parameters;

public interface IDataHeaderParameters<TProperty, TEntity, TValue> : IParameters
    where TProperty : IDataHeaderProperty<TEntity, TValue>
{
    TProperty Property { get; }

    IDataHeaderOptions Options { get; }

    IDataHeaderOrigin<TEntity> Origin { get; }

    IEnumerable<IRenderer> GetHeaderItems(BaseDataHeader<TProperty, TEntity, TValue> header);
}