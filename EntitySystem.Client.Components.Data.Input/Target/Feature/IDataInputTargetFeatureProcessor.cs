using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Components.Data.Input.Parameters;
using EntitySystem.Client.Components.Data.Input.Target.Property;

namespace EntitySystem.Client.Components.Data.Input.Target.Feature;

public interface IDataInputTargetFeatureProcessor : IRegistrable
{
    public void Process<TParent, TChild>(IDataInputParameters<IDataInputTargetProperty<TParent, TChild>, TParent, TChild> parameters);
}