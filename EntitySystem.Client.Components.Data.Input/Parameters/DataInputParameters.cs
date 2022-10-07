using EntitySystem.Client.Abstract.Components;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Options;
using EntitySystem.Client.Components.Data.Input.Property;

namespace EntitySystem.Client.Components.Data.Input.Parameters;

public abstract class DataInputParameters<TProperty, TEntity, TValue> : Featured, IDataInputParameters<TProperty, TEntity, TValue>
    where TProperty : IDataInputProperty<TEntity, TValue>
{
    public TProperty Property { get; }

    public TEntity Entity { get; }

    public IDataInputOptions Options { get; }

    protected DataInputParameters(TProperty property, TEntity entity, IDataInputOptions options)
    {
        Property = property;
        Entity = entity;
        Options = options;
    }

    public abstract IRenderer BuildInputValue(BaseDataInput<TProperty, TEntity, TValue> input);

    public IRenderer Build<TComponent>() where TComponent : BaseRendered<IDataInputParameters<TProperty, TEntity, TValue>>
    {
        return new Renderer<IDataInputParameters<TProperty, TEntity, TValue>, TComponent>(this, Property.Priority);
    }
}