using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Input.Decimal.Factory;
using EntitySystem.Client.Components.Data.Input.Decimal.Property;
using EntitySystem.Client.Components.Data.Input.Options;
using EntitySystem.Client.Components.Data.Input.Parameters;

namespace EntitySystem.Client.Components.Data.Input.Decimal.Parameters;

public class DataInputDecimalParameters<TEntity> : DataInputParameters<IDataInputDecimalProperty<TEntity>, TEntity, decimal>
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataInputDecimalParameters(IRegistrationProvider registrationProvider, IDataInputDecimalProperty<TEntity> property, TEntity entity, IDataInputOptions options) : base(property, entity, options)
    {
        _registrationProvider = registrationProvider;
    }

    public override IRenderer BuildInputValue(BaseDataInput<IDataInputDecimalProperty<TEntity>, TEntity, decimal> input)
    {
        var factory = _registrationProvider.GetRegistration<IDataInputDecimalFactory>();

        return factory.Build(input);
    }
}