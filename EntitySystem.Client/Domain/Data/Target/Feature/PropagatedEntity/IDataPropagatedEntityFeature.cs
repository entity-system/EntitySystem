using System.Threading.Tasks;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Domain.Data.Pair;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Target.Feature.PropagatedEntity;

public interface IDataPropagatedEntityFeature : IFeature
{
    Task OnAfterInitializeEntityAsync<TEntity>(IDataSource<TEntity> source, TEntity entity)
        where TEntity : IEntity;

    Task OnAfterCreateChildAsync<TParent, TChild>(IDataPair<TParent, TChild> join, TParent parent, TChild child)
        where TParent : IEntity
        where TChild : IEntity;
}