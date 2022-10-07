using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Record.Info.Source;
using EntitySystem.Client.Components.Data.Table.Join.Options;

namespace EntitySystem.Client.Components.Data.Record.Info.Join;

public interface IDataRecordInfoJoin<in TKey> : IFeatured
{
    IDataRecordInfoSource GetRecordInfoChildSource();

    IRenderer BuildTableJoin(IDataTableJoinOptions<TKey> options);
}