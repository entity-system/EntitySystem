using EntitySystem.Client.Components.Data.Entity.Dialog.Feature.InstantAdd;
using EntitySystem.Client.Components.Data.Record.Info.Feature.DeleteButton;
using EntitySystem.Client.Components.Data.Record.Info.Feature.EditButton;
using EntitySystem.Client.Components.Data.Record.List.Feature.AddButton;
using EntitySystem.Client.Components.Data.Record.List.Feature.EditButton;
using EntitySystem.Client.Components.Data.Record.List.Feature.Limit;
using EntitySystem.Client.Components.Data.Record.List.Feature.RemoveButton;
using EntitySystem.Client.Components.Data.Record.List.Feature.SelectBox;

namespace EntitySystem.Client.Domain.Data.Source.Extensions.Settings;

public static class DataSourceSettingsExtensions
{
    public static TDataSource InstantAdd<TDataSource>(this TDataSource dataSource)
        where TDataSource : IDataSource
    {
        dataSource.Features.AddFeature(new DataEntityDialogEnableInstantAddFeature());

        return dataSource;
    }

    public static TDataSource DisableSelect<TDataSource>(this TDataSource dataSource)
        where TDataSource : IDataSource
    {
        dataSource.Features.AddFeature(new DataRecordLisDisableSelectFeature());

        return dataSource;
    }

    public static TDataSource DisableAdd<TDataSource>(this TDataSource dataSource)
        where TDataSource : IDataSource
    {
        dataSource.Features.AddFeature(new DataRecordListDisableAddFeature());

        return dataSource;
    }

    public static TDataSource Single<TDataSource>(this TDataSource dataSource)
        where TDataSource : IDataSource
    {
        return dataSource.Limit(1);
    }

    public static TDataSource Limit<TDataSource>(this TDataSource dataSource, int limit)
        where TDataSource : IDataSource
    {
        dataSource.Features.AddFeature(new DataRecordListEnableLimitFeature(limit));

        return dataSource;
    }

    public static TDataSource DisableEdit<TDataSource>(this TDataSource dataSource)
        where TDataSource : IDataSource
    {
        dataSource.Features.AddFeature(new DataRecordInfoDisableEditFeature());

        dataSource.Features.AddFeature(new DataRecordListDisableEditFeature());

        return dataSource;
    }

    public static TDataSource DisableDelete<TDataSource>(this TDataSource dataSource)
        where TDataSource : IDataSource
    {
        dataSource.Features.AddFeature(new DataRecordInfoDisableDeleteFeature());

        dataSource.Features.AddFeature(new DataRecordListDisableRemoveFeature());

        return dataSource;
    }
}