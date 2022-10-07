using System;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Input.Date.Factory;
using EntitySystem.Client.Components.Data.Input.Date.Property;
using EntitySystem.Client.Components.Data.Input.Options;
using EntitySystem.Client.Components.Data.Input.Parameters;

namespace EntitySystem.Client.Components.Data.Input.Date.Parameters;

public class DataInputDateParameters<TEntity> : DataInputParameters<IDataInputDateProperty<TEntity>, TEntity, DateTime?>
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataInputDateParameters(IRegistrationProvider registrationProvider, IDataInputDateProperty<TEntity> property, TEntity entity, IDataInputOptions options) : base(property, entity, options)
    {
        _registrationProvider = registrationProvider;
    }

    public override IRenderer BuildInputValue(BaseDataInput<IDataInputDateProperty<TEntity>, TEntity, DateTime?> input)
    {
        var factory = _registrationProvider.GetRegistration<IDataInputDateFactory>();

        return factory.Build(input);
    }
}