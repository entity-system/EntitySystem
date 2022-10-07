using EntitySystem.Client.Abstract.Domain.Parameters;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Options;
using EntitySystem.Client.Components.Data.Input.Property;

namespace EntitySystem.Client.Components.Data.Input.Parameters;

public interface IDataInputParameters<TProperty, TEntity, TValue> : IParameters
    where TProperty : IDataInputProperty<TEntity, TValue>
   
{
    TProperty Property { get; }

    TEntity Entity { get; }

    IDataInputOptions Options { get; }

    IRenderer BuildInputValue(BaseDataInput<TProperty, TEntity, TValue> input);
}