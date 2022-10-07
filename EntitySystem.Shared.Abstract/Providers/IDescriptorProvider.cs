using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace EntitySystem.Shared.Abstract.Providers;

public interface IDescriptorProvider
{
    IEnumerable<ServiceDescriptor> GetDescriptionsOfType<T>();
}