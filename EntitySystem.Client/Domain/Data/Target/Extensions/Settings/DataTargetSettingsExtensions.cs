using EntitySystem.Client.Components.Data.Input.Target.Feature.CreateItem;
using EntitySystem.Client.Components.Data.Input.Target.Feature.ImplicitCreate;
using EntitySystem.Client.Components.Data.Input.Target.Feature.ImplicitEdit;

namespace EntitySystem.Client.Domain.Data.Target.Extensions.Settings;

public static class DataTargetSettingsExtensions
{
    public static IDataTarget ImplicitCreate(this IDataTarget target)
    {
        target.Features.AddFeature(new DataInputTargetEnableImplicitCreateFeature());

        return target;
    }

    public static IDataTarget ImplicitEdit(this IDataTarget target)
    {
        target.Features.AddFeature(new DataInputTargetEnableImplicitEditFeature());

        return target;
    }

    public static IDataTarget DisableCreate(this IDataTarget target)
    {
        target.Features.AddFeature(new DataInputTargetDisableCreateFeature());

        return target;
    }
}