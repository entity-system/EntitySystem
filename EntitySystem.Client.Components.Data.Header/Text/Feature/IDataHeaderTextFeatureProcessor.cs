using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Components.Data.Header.Parameters;
using EntitySystem.Client.Components.Data.Header.Text.Property;

namespace EntitySystem.Client.Components.Data.Header.Text.Feature;

public interface IDataHeaderTextFeatureProcessor : IRegistrable
{
    void Process<TEntity>(IDataHeaderParameters<IDataHeaderTextProperty<TEntity>, TEntity, string> parameters);
}