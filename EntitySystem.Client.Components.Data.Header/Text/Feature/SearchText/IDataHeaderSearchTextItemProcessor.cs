using EntitySystem.Client.Abstract.Domain.Registrations;

namespace EntitySystem.Client.Components.Data.Header.Text.Feature.SearchText;

public interface IDataHeaderSearchTextItemProcessor : IRegistrable
{
    void Process<TEntity>(DataHeaderSearchTextParameters<TEntity> parameters);
}