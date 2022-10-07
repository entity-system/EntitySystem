using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Pair;

public interface IDataPair : IFeatured
{
    IDataSource GetChildSource();

    IEnumerable<IEntity> GetPairedEntities(IEntity entity);
}

public interface IDataPair<TParent, TChild> : IDataPair
    where TParent : IEntity
    where TChild : IEntity

{
    IDataSource<TParent> GetParentEntitySource();

    IDataSource<TChild> GetChildEntitySource();
}