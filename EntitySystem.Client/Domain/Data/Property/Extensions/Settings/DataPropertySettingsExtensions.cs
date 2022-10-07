using System;
using EntitySystem.Client.Components.Data.Entity.Dialog.Feature.HidePropertyInAdd;
using EntitySystem.Client.Components.Data.Entity.Dialog.Feature.HidePropertyInEdit;
using EntitySystem.Client.Components.Data.Input.Feature;
using EntitySystem.Client.Components.Data.Record.Info.Feature.HideFilter;
using EntitySystem.Client.Components.Data.Record.List.Feature.DeepMaximumFilter;
using EntitySystem.Client.Components.Data.Record.List.Feature.HideFilter;
using EntitySystem.Client.Components.Data.Record.List.Feature.ShowInJoinTabOnly;

namespace EntitySystem.Client.Domain.Data.Property.Extensions.Settings;

public static class DataPropertySettingsExtensions
{
    public static IDataProperty ReadOnlyInput(this IDataProperty property)
    {
        property.Features.AddFeature(new DataInputReadOnlyFeature());

        return property;
    }

    public static IDataProperty HideInListAndInfoAndEntityDialog(this IDataProperty property)
    {
        property
            .HideInList()
            .HideInInfo()
            .HideInEntityDialog();

        return property;
    }

    public static IDataProperty HideInListAndEntityDialog(this IDataProperty property)
    {
        property
            .HideInList()
            .HideInEntityDialog();

        return property;
    }

    public static IDataProperty HideInInfo(this IDataProperty property)
    {
        property.Features.AddFeature(new DataRecordInfoHidePropertyFilter());

        return property;
    }

    public static IDataProperty HideInList(this IDataProperty property)
    {
        property.Features.AddFeature(new DataRecordListHidePropertyFilter());

        return property;
    }

    public static IDataProperty HideInEntityDialog(this IDataProperty property)
    {
        return property
            .HideInAddDialog()
            .HideInEditDialog();
    }

    public static IDataProperty HideInAddDialog(this IDataProperty property)
    {
        var feature = property.GetDataSource().RegistrationProvider.GetRegistration<IDataEntityDialogHidePropertyInAddFeature>();

        property.Features.AddFeature(feature);

        return property;
    }

    public static IDataProperty HideInEditDialog(this IDataProperty property)
    {
        var feature = property.GetDataSource().RegistrationProvider.GetRegistration<IDataEntityDialogHidePropertyInEditFeature>();

        property.Features.AddFeature(feature);

        return property;
    }

    public static IDataProperty HideByDeepMaximum(this IDataProperty property, int deepMaximum = 0)
    {
        property.Features.AddFeature(new DataRecordListHidePropertyByDeepMaximumFilter { DeepMaximum = deepMaximum });

        return property;
    }

    public static IDataProperty ShowInJoinTabOnly(this IDataProperty property)
    {
        property.Features.AddFeature(new DataRecordListShowInJoinTabOnlyFilter());

        return property;
    }

    public static IDataProperty Next(this IDataProperty property, Action<IDataProperty> next)
    {
        next?.Invoke(property);

        return property;
    }
}