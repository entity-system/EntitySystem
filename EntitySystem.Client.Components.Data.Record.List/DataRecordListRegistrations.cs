using EntitySystem.Client.Components.Data.Record.List.Factory;
using EntitySystem.Client.Components.Data.Record.List.Feature.AddButton;
using EntitySystem.Client.Components.Data.Record.List.Feature.ConditionLabel;
using EntitySystem.Client.Components.Data.Record.List.Feature.EditButton;
using EntitySystem.Client.Components.Data.Record.List.Feature.OrderLabel;
using EntitySystem.Client.Components.Data.Record.List.Feature.Pager;
using EntitySystem.Client.Components.Data.Record.List.Feature.RefreshButton;
using EntitySystem.Client.Components.Data.Record.List.Feature.RemoveButton;
using EntitySystem.Client.Components.Data.Record.List.Feature.SelectBox;
using EntitySystem.Client.Components.Data.Record.List.Feature.Title;
using EntitySystem.Client.Components.Data.Record.List.Feature.Value;
using EntitySystem.Client.Components.Data.Record.List.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EntitySystem.Client.Components.Data.Record.List;

public static class DataRecordListRegistrations
{
    public static IServiceCollection AddDataRecordListRegistrations(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IDataRecordListNavigationService, DataRecordListNavigationService>()
            .AddSingleton<IDataRecordListFactory, DataRecordListFactory>()
            .AddSingleton<IDataRecordListTitleProcessor, DataRecordListTitleProcessor>()
            .AddSingleton<IDataRecordListTitleFeature, DataRecordListTitleFeature>()
            .AddSingleton<IDataRecordListRefreshButtonProcessor, DataRecordListRefreshButtonProcessor>()
            .AddSingleton<IDataRecordListRefreshButtonFeature, DataRecordListRefreshButtonFeature>()
            .AddSingleton<IDataRecordListAddButtonProcessor, DataRecordListAddButtonProcessor>()
            .AddSingleton<IDataRecordListAddButtonFeature, DataRecordListAddButtonFeature>()
            .AddSingleton<IDataRecordListEditButtonProcessor, DataRecordListEditButtonProcessor>()
            .AddSingleton<IDataRecordListEditButtonFeature, DataRecordListEditButtonFeature>()
            .AddSingleton<IDataRecordListRemoveButtonProcessor, DataRecordListRemoveButtonProcessor>()
            .AddSingleton<IDataRecordListRemoveButtonFeature, DataRecordListRemoveButtonFeature>()
            .AddSingleton<IDataRecordListOrderLabelProcessor, DataRecordListOrderLabelProcessor>()
            .AddSingleton<IDataRecordListOrderLabelFeature, DataRecordListOrderLabelFeature>()
            .AddSingleton<IDataRecordListConditionLabelProcessor, DataRecordListConditionLabelProcessor>()
            .AddSingleton<IDataRecordListConditionLabelFeature, DataRecordListConditionLabelFeature>()
            .AddSingleton<IDataRecordListPagerProcessor, DataRecordListPagerProcessor>()
            .AddSingleton<IDataRecordListPagerFeature, DataRecordListPagerFeature>()
            .AddSingleton<IDataRecordListSelectBoxProcessor, DataRecordListSelectBoxProcessor>()
            .AddSingleton<IDataRecordListSelectBoxFeature, DataRecordListSelectBoxFeature>()
            .AddSingleton<IDataRecordListValueProcessor, DataRecordListValueProcessor>()
            .AddSingleton<IDataRecordListValueFeature, DataRecordListValueFeature>();
    }
}