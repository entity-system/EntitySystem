using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Options;

namespace EntitySystem.Client.Components.Data.Record.Info.Property;

public interface IDataRecordInfoProperty : IFeatured
{
    long Priority { get; }
}

public interface IDataRecordInfoProperty<in TKey> : IDataRecordInfoProperty
{
    IRenderer BuildInput(TKey key, IDataInputOptions option);
}