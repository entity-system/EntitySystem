using System;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Domain.Data.Property.Extensions;
using EntitySystem.Client.Domain.Data.Source.Feature;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Source.Factory;

public class DataSourceFactory : IDataSourceFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IRegistrationProvider _registrationProvider;

    public DataSourceFactory(IServiceProvider serviceProvider, IRegistrationProvider registrationProvider)
    {
        _serviceProvider = serviceProvider;
        _registrationProvider = registrationProvider;
    }

    public DataSource<TEntity, TService> Build<TEntity, TService>(string type, int targetDeep, int joinDeep)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        var dataSource = new DataSource<TEntity, TService>(_serviceProvider, type, targetDeep, joinDeep);

        foreach (var processor in _registrationProvider.GetRegistrationsOf<IDataSourceFeatureProcessor>())
        {
            processor.Process(dataSource);
        }

        if (targetDeep == 0) dataSource.Identifier("ID", e => e.Id);

        return dataSource;
    }
}