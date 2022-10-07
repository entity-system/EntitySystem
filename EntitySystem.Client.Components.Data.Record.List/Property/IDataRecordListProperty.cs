using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Header.Options;

namespace EntitySystem.Client.Components.Data.Record.List.Property;

public interface IDataRecordListProperty : IFeatured
{
    long Priority { get; }

    int TargetDeep { get; }

    int JoinDeep { get; }

    IRenderer BuildHeader(IDataHeaderOptions options);
}