using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EntitySystem.Client.Domain.Data.Condition;
using EntitySystem.Client.Domain.Data.Pair;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Feature.ReverseCloneJoin.Source;

public static class DataSourceReverseCloneJoinExtensions
{
    public static DataSource<TEntity, TService> Where<TEntity, TService, TReference, TReferenceService>(this DataSource<TEntity, TService> source, DataSource<TReference, TReferenceService> referenceSource, Expression<Func<TReference, bool>> equals/*, IDataSource scope = null*/, string name = null)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
        where TReference : IEntity, new()
        where TReferenceService : IEntityService<TReference>
    {
        var feature = source.Features.GetFeature<IDataSourceReverseCloneJoinFeature>();

        var targetSource = source.Cloned != null ? feature.ReverseCloneJoin<TReference, TReferenceService>(FindPath()) : referenceSource;

        var condition = new DataCondition<TReference>(equals, name);

        condition.Assign(targetSource);

        return source;

        Stack<IDataPair> FindPath() => new(referenceSource.Features.GetFeature<IDataSourceReverseCloneJoinFeature>().FindPathsBySource(source).SingleOrDefault() ?? Enumerable.Empty<IDataPair>());
    }
}