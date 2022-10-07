using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Entity.Dialog.Feature;
using EntitySystem.Client.Components.Data.Entity.Dialog.Parameters;

namespace EntitySystem.Client.Components.Data.Entity.Dialog.Factory;

public class DataEntityDialogFactory : IDataEntityDialogFactory
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataEntityDialogFactory(IRegistrationProvider registrationProvider)
    {
        _registrationProvider = registrationProvider;
    }

    public virtual IRenderer Build<TEntity>(DataEntityDialogParameters<TEntity> parameters)
    {
        foreach (var processor in _registrationProvider.GetRegistrationsOf<IDataEntityDialogFeatureProcessor>())
        {
            processor.Process(parameters);
        }

        return new Renderer<IDataEntityDialogParameters<TEntity>, DataEntityDialog<TEntity>>(parameters);
    }
}