using System;
using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Join;
using EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Pair;
using EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Target;
using EntitySystem.Client.Domain.Data.Join;
using EntitySystem.Client.Domain.Data.Pair;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Client.Domain.Data.Target;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Source;

public class DataSourceReverseCloneJoinFeature<TEntity, TService> : IDataSourceReverseCloneJoinFeature
    where TEntity : IEntity, new()
    where TService : IEntityService<TEntity>
{
    public DataSource<TEntity, TService> Source { get; }

    public DataSourceReverseCloneJoinFeature(DataSource<TEntity, TService> source)
    {
        Source = source;
    }

    public IEnumerable<IEnumerable<IDataPair>> FindPathsBySource(IDataSource source, List<IDataPair> path = null)
    {
        if (Source == source || Source == source.Cloned || Source.Cloned == source || Source.Cloned == source.Cloned) yield return path;

        foreach (var inner in Source.Targets.OfType<IDataPair>().Union(Source.Joins).SelectMany(GetPath)) yield return inner;

        IEnumerable<IEnumerable<IDataPair>> GetPath(IDataPair pair) => Featured(pair.GetChildSource()).FindPathsBySource(source, new List<IDataPair>(path ?? Enumerable.Empty<IDataPair>()) { pair });
    }

    public IDataSourceReverseCloneJoinFeature Featured(IDataSource source)
    {
        return source.Features.GetFeature<IDataSourceReverseCloneJoinFeature>();
    }

    public DataSource<TReverseEntity, TReverseService> ReverseCloneJoin<TReverseEntity, TReverseService>(Stack<IDataPair> path)
        where TReverseEntity : IEntity, new()
        where TReverseService : IEntityService<TReverseEntity>
    {
        if (!path.TryPop(out var pair)) return Source as DataSource<TReverseEntity, TReverseService>;

        IDataPairReverseCloneJoinFeature<TEntity, TService> feature = pair switch
        {
            IDataTarget => pair.Features.GetFeature<IDataTargetReverseCloneJoinFeature<TEntity, TService>>(),
            IDataJoin => pair.Features.GetFeature<IDataJoinReverseCloneJoinFeature<TEntity, TService>>(),
            _ => throw new InvalidOperationException("Invalid data pair.")
        };

        return feature.ReverseCloneJoin<TReverseEntity, TReverseService>(Source, path);
    }
}