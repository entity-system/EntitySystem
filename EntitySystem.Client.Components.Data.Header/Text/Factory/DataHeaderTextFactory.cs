using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Header.Factory;
using EntitySystem.Client.Components.Data.Header.Parameters;
using EntitySystem.Client.Components.Data.Header.Text.Feature;
using EntitySystem.Client.Components.Data.Header.Text.Property;

namespace EntitySystem.Client.Components.Data.Header.Text.Factory;

public class DataHeaderTextFactory : DataHeaderFactory, IDataHeaderTextFactory
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataHeaderTextFactory(IRegistrationProvider registrationProvider) : base(registrationProvider)
    {
        _registrationProvider = registrationProvider;
    }

    public IRenderer Build<TEntity>(DataHeaderParameters<IDataHeaderTextProperty<TEntity>, TEntity, string> parameters)
    {
        foreach (var processor in _registrationProvider.GetRegistrationsOf<IDataHeaderTextFeatureProcessor>())
        {
            processor.Process(parameters);
        }

        return Build<IDataHeaderTextProperty<TEntity>, TEntity, string>(parameters);
    }
}