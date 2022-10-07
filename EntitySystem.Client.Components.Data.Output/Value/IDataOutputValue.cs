using EntitySystem.Client.Components.Data.Output.Object;
using EntitySystem.Client.Components.Data.Output.Property;

namespace EntitySystem.Client.Components.Data.Output.Value;

public interface IDataOutputValue<TKey, TEntity, out TValue>
{
    IDataOutputObject<TKey, TEntity> GetOutputObject();

    IDataOutputProperty<TEntity, TValue> GetOutputProperty();
}