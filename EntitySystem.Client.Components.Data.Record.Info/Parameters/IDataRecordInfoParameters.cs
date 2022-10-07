using EntitySystem.Client.Abstract.Domain.Parameters;
using EntitySystem.Client.Components.Data.Record.Info.Options;

namespace EntitySystem.Client.Components.Data.Record.Info.Parameters;

public interface IDataRecordInfoParameters<TKey> : IParameters
{
    IDataRecordInfoOptions<TKey> Options { get; }
}