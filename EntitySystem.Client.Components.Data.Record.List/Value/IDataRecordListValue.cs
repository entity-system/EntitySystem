using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Output.Options;
using EntitySystem.Client.Components.Data.Record.List.Property;

namespace EntitySystem.Client.Components.Data.Record.List.Value;

public interface IDataRecordListValue
{
    IDataRecordListProperty GetRecordListProperty();

    IRenderer BuildOutput(IDataOutputOptions options);
}