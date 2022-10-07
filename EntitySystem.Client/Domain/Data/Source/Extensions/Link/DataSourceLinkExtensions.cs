using System;
using EntitySystem.Client.Components.Data.Output.Feature.LinkFormatter;
using EntitySystem.Client.Components.Data.Record.Info.Feature.BackButton;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Source.Extensions.Link;

public static class DataSourceLinkExtensions
{
    public static DataSource<TEntity, TService> LinkBack<TEntity, TService>(this DataSource<TEntity, TService> source)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        return source.LinkBack("../");
    }

    public static DataSource<TEntity, TService> LinkBack<TEntity, TService>(this DataSource<TEntity, TService> source, string link)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        return source.LinkBack(_ => link);
    }

    public static DataSource<TEntity, TService> LinkBack<TEntity, TService>(this DataSource<TEntity, TService> source, Func<TEntity, string> linkFactory)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        source.Features.AddFeature(new DataRecordInfoEnableBackFeature<TEntity>(linkFactory));

        return source;
    }

    public static DataSource<TEntity, TService> LinkForward<TEntity, TService>(this DataSource<TEntity, TService> source)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        return source.LinkForward(e => $"./{e.Id}");
    }

    public static DataSource<TEntity, TService> LinkForward<TEntity, TService>(this DataSource<TEntity, TService> source, Func<TEntity, string> linkFactory, string linkTarget = "_self")
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        source.Features.AddFeature(new DataOutputEnableLink<TEntity>(linkFactory, linkTarget));

        return source;
    }
}