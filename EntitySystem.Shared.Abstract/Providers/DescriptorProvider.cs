using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace EntitySystem.Shared.Abstract.Providers;

public class DescriptorProvider : IDescriptorProvider
{
    private readonly IServiceCollection _serviceCollection;

    public DescriptorProvider(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    public IEnumerable<ServiceDescriptor> GetDescriptionsOfType<T>()
    {
        return _serviceCollection.Where(d => d.ServiceType.IsAssignableTo(typeof(T)));
    }
}