using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Input.Text.Factory;
using EntitySystem.Client.Components.Data.Input.Text.Property;
using EntitySystem.Client.Components.Data.Input.Options;
using EntitySystem.Client.Components.Data.Input.Parameters;

namespace EntitySystem.Client.Components.Data.Input.Text.Parameters;

public class DataInputTextParameters<TEntity> : DataInputParameters<IDataInputTextProperty<TEntity>, TEntity, string>
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataInputTextParameters(IRegistrationProvider registrationProvider, IDataInputTextProperty<TEntity> property, TEntity entity, IDataInputOptions options) : base(property, entity, options)
    {
        _registrationProvider = registrationProvider;
    }

    public override IRenderer BuildInputValue(BaseDataInput<IDataInputTextProperty<TEntity>, TEntity, string> input)
    {
        var factory = _registrationProvider.GetRegistration<IDataInputTextFactory>();

        return factory.Build(input);
    }
}