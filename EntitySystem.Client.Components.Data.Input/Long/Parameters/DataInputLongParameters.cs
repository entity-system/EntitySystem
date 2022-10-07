using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Input.Long.Factory;
using EntitySystem.Client.Components.Data.Input.Long.Property;
using EntitySystem.Client.Components.Data.Input.Options;
using EntitySystem.Client.Components.Data.Input.Parameters;

namespace EntitySystem.Client.Components.Data.Input.Long.Parameters;

public class DataInputLongParameters<TEntity> : DataInputParameters<IDataInputLongProperty<TEntity>, TEntity, long>
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataInputLongParameters(IRegistrationProvider registrationProvider, IDataInputLongProperty<TEntity> property, TEntity entity, IDataInputOptions options) : base(property, entity, options)
    {
        _registrationProvider = registrationProvider;
    }

    public override IRenderer BuildInputValue(BaseDataInput<IDataInputLongProperty<TEntity>, TEntity, long> input)
    {
        var factory = _registrationProvider.GetRegistration<IDataInputLongFactory>();

        return factory.Build(input);
    }
}