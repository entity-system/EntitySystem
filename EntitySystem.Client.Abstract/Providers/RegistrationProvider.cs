using System;
using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Shared.Abstract.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace EntitySystem.Client.Abstract.Providers;

public class RegistrationProvider : IRegistrationProvider
{
    private readonly IDescriptorProvider _descriptorProvider;
    private readonly IServiceProvider _serviceProvider;

    public RegistrationProvider(IDescriptorProvider descriptorProvider, IServiceProvider serviceProvider)
    {
        _descriptorProvider = descriptorProvider;
        _serviceProvider = serviceProvider;
    }

    public IEnumerable<T> GetRegistrationsOf<T>()
        where T : IRegistrable
    {
        var descriptors = _descriptorProvider.GetDescriptionsOfType<T>();

        foreach (var serviceType in descriptors.Select(d => d.ServiceType))
        {
            yield return (T)_serviceProvider.GetService(serviceType);
        }
    }

    public T GetRegistration<T>()
        where T : IRegistrable
    {
        return _serviceProvider.GetService<T>();
    }
}