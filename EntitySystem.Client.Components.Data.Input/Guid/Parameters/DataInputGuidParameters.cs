using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Input.Guid.Factory;
using EntitySystem.Client.Components.Data.Input.Guid.Property;
using EntitySystem.Client.Components.Data.Input.Options;
using EntitySystem.Client.Components.Data.Input.Parameters;

namespace EntitySystem.Client.Components.Data.Input.Guid.Parameters;

public class DataInputGuidParameters<TEntity> : DataInputParameters<IDataInputGuidProperty<TEntity>, TEntity, System.Guid>
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataInputGuidParameters(IRegistrationProvider registrationProvider, IDataInputGuidProperty<TEntity> property, TEntity entity, IDataInputOptions options) : base(property, entity, options)
    {
        _registrationProvider = registrationProvider;
    }

    public override IRenderer BuildInputValue(BaseDataInput<IDataInputGuidProperty<TEntity>, TEntity, System.Guid> input)
    {
        var factory = _registrationProvider.GetRegistration<IDataInputGuidFactory>();

        return factory.Build(input);
    }
}