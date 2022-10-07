using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Header.Feature;
using EntitySystem.Client.Components.Data.Header.Options;
using EntitySystem.Client.Components.Data.Header.Origin;
using EntitySystem.Client.Components.Data.Header.Property;

namespace EntitySystem.Client.Components.Data.Header.Parameters;

public class DataHeaderParameters<TProperty, TEntity, TValue> : Featured, IDataHeaderParameters<TProperty, TEntity, TValue>
    where TProperty : IDataHeaderProperty<TEntity, TValue>
{
    public TProperty Property { get; set; }

    public IDataHeaderOptions Options { get; set; }

    public IDataHeaderOrigin<TEntity> Origin { get; set; }

    public DataHeaderParameters(TProperty property, IDataHeaderOptions options, IDataHeaderOrigin<TEntity> origin)
    {
        Property = property;
        Options = options;
        Origin = origin;
    }

    public virtual IEnumerable<IRenderer> GetHeaderItems(BaseDataHeader<TProperty, TEntity, TValue> header)
    {
        return Features.GetFeatures<IDataHeaderItemFeature>().SelectMany(f => f.Build(header));
    }
}