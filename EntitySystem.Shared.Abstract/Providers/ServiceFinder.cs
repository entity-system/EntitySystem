using System;
using System.Collections.Generic;
using System.Linq;

namespace EntitySystem.Shared.Abstract.Providers;

    public class ServiceFinder : IServiceFinder
    {
        private readonly IDescriptorProvider _descriptorProvider;
        private readonly IServiceProvider _serviceProvider;

        public ServiceFinder(IDescriptorProvider descriptorProvider, IServiceProvider serviceProvider)
        {
            _descriptorProvider = descriptorProvider;
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<T> Search<T>()
        {
            var descriptors = _descriptorProvider.GetDescriptionsOfType<T>();

            foreach (var type in descriptors.Select(d => d.ServiceType))
            {
                yield return (T)_serviceProvider.GetService(type) ?? throw new InvalidOperationException("Invalid service.");
            }
        }
    }

