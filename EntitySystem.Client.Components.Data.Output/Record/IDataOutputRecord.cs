using EntitySystem.Client.Components.Data.Output.Source;

namespace EntitySystem.Client.Components.Data.Output.Record;

public interface IDataOutputRecord<TKey>
{
    TKey Key { get; }

    IDataOutputSource<TKey> GetOutputSource();
}