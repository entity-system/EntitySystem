using EntitySystem.Client.Abstract.Domain.Parameters;
using EntitySystem.Client.Components.Data.Record.List.Options;

namespace EntitySystem.Client.Components.Data.Record.List.Parameters;

public interface IDataRecordListParameters<TKey> : IParameters
{
    IDataRecordListOptions<TKey> Options { get; }
}