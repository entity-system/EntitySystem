using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Input.Target.Factory;
using EntitySystem.Client.Components.Data.Input.Target.Property;
using EntitySystem.Client.Components.Data.Input.Options;
using EntitySystem.Client.Components.Data.Input.Parameters;

namespace EntitySystem.Client.Components.Data.Input.Target.Parameters;

public class DataInputTargetParameters<TParent, TChild> : DataInputParameters<IDataInputTargetProperty<TParent, TChild>, TParent, TChild>
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataInputTargetParameters(IRegistrationProvider registrationProvider, IDataInputTargetProperty<TParent, TChild> property, TParent entity, IDataInputOptions options) : base(property, entity, options)
    {
        _registrationProvider = registrationProvider;
    }

    public override IRenderer BuildInputValue(BaseDataInput<IDataInputTargetProperty<TParent, TChild>, TParent, TChild> input)
    {
        var factory = _registrationProvider.GetRegistration<IDataInputTargetFactory>();

        return factory.Build(input);
    }
}