using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Input.Target.Feature;
using EntitySystem.Client.Components.Data.Input.Target.Property;

namespace EntitySystem.Client.Components.Data.Input.Target.Factory;

public class DataInputTargetFactory : IDataInputTargetFactory
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataInputTargetFactory(IRegistrationProvider registrationProvider)
    {
        _registrationProvider = registrationProvider;
    }

    public IRenderer Build<TParent, TChild>(BaseDataInput<IDataInputTargetProperty<TParent, TChild>, TParent, TChild> input)
    {
        foreach (var processor in _registrationProvider.GetRegistrationsOf<IDataInputTargetFeatureProcessor>())
        {
            processor.Process(input.Parameters);
        }

        return new Renderer<BaseDataInput<IDataInputTargetProperty<TParent, TChild>, TParent, TChild>, DataInputTarget<TParent, TChild>>(input);
    }
}