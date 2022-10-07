using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Header.Options;
using EntitySystem.Client.Components.Data.Header.Origin;
using EntitySystem.Client.Components.Data.Header.Parameters;
using EntitySystem.Client.Components.Data.Header.Text.Feature;
using EntitySystem.Client.Components.Data.Header.Text.Property;

namespace EntitySystem.Client.Components.Data.Header.Text.Parameters;

public class DataHeaderTextParameters<TEntity> : DataHeaderParameters<IDataHeaderTextProperty<TEntity>, TEntity, string>
{
    public DataHeaderTextParameters(IDataHeaderTextProperty<TEntity> property, IDataHeaderOptions options, IDataHeaderOrigin<TEntity> origin) : base(property, options, origin)
    {
    }

    public override IEnumerable<IRenderer> GetHeaderItems(BaseDataHeader<IDataHeaderTextProperty<TEntity>, TEntity, string> header)
    {
        foreach (var renderer in base.GetHeaderItems(header)) yield return renderer;

        foreach (var renderer in Features.GetFeatures<IDataHeaderTextItemFeature>().SelectMany(f => f.Build(header))) yield return renderer;
    }
}