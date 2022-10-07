using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Parameters;
using EntitySystem.Client.Components.Data.Input.Property;

namespace EntitySystem.Client.Components.Data.Input.Factory;

public class DataInputFactory : IDataInputFactory
{
    public IRenderer Build<TProperty, TEntity, TValue>(DataInputParameters<TProperty, TEntity, TValue> parameters)
        where TProperty : IDataInputProperty<TEntity, TValue>
    {
        return parameters.Build<DataInput<TProperty, TEntity, TValue>>();
    }
}