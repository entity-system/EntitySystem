using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Header.Text.Property;

namespace EntitySystem.Client.Components.Data.Header.Text.Feature.SearchText;

public class DataHeaderSearchTextFeature : IDataHeaderSearchTextFeature
{
    private readonly IRegistrationProvider _registrationProvider;

    public DataHeaderSearchTextFeature(IRegistrationProvider registrationProvider)
    {
        _registrationProvider = registrationProvider;
    }

    public IEnumerable<IRenderer> Build<TEntity>(BaseDataHeader<IDataHeaderTextProperty<TEntity>, TEntity, string> header)
    {
        var parameters = new DataHeaderSearchTextParameters<TEntity>(header);

        foreach (var processor in _registrationProvider.GetRegistrationsOf<IDataHeaderSearchTextItemProcessor>())
        {
            processor.Process(parameters);
        }

        yield return new Renderer<DataHeaderSearchTextParameters<TEntity>, DataHeaderSearchText<TEntity>>(parameters);
    }
}