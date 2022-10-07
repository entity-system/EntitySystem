using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Input.Integer.Factory;
using EntitySystem.Client.Components.Data.Input.Integer.Property;
using EntitySystem.Client.Components.Data.Input.Options;
using EntitySystem.Client.Components.Data.Input.Parameters;

namespace EntitySystem.Client.Components.Data.Input.Integer.Parameters;

public class DataInputIntegerParameters<TEntity> : DataInputParameters<IDataInputIntegerProperty<TEntity>, TEntity, int>
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataInputIntegerParameters(IRegistrationProvider registrationProvider, IDataInputIntegerProperty<TEntity> property, TEntity entity, IDataInputOptions options) : base(property, entity, options)
    {
        _registrationProvider = registrationProvider;
    }

    public override IRenderer BuildInputValue(BaseDataInput<IDataInputIntegerProperty<TEntity>, TEntity, int> input)
    {
        var factory = _registrationProvider.GetRegistration<IDataInputIntegerFactory>();

        return factory.Build(input);
    }
}