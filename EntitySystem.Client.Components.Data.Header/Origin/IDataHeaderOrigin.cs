namespace EntitySystem.Client.Components.Data.Header.Origin;

public interface IDataHeaderOrigin<in TEntity>
{
    void Restrict(TEntity entity);

    void Order(bool descending);
}