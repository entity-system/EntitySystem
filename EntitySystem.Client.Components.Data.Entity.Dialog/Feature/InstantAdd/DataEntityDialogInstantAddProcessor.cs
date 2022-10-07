using EntitySystem.Client.Components.Data.Entity.Dialog.Parameters;

namespace EntitySystem.Client.Components.Data.Entity.Dialog.Feature.InstantAdd;

public class DataEntityDialogInstantAddProcessor : IDataEntityDialogInstantAddProcessor
{
    private readonly IDataEntityDialogInstantAddFeature _instantAddFeature;

    public DataEntityDialogInstantAddProcessor(IDataEntityDialogInstantAddFeature instantAddFeature)
    {
        _instantAddFeature = instantAddFeature;
    }

    public void Process<TEntity>(IDataEntityDialogParameters<TEntity> parameters)
    {
        if (!parameters.Options.GetEntityDialogSource().Features.HasFeature<DataEntityDialogEnableInstantAddFeature>()) return;

        parameters.Features.AddFeature(_instantAddFeature);
    }
}