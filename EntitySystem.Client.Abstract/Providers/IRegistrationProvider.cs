using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Registrations;

namespace EntitySystem.Client.Abstract.Providers;

public interface IRegistrationProvider
{
    IEnumerable<T> GetRegistrationsOf<T>()
        where T : IRegistrable;

    T GetRegistration<T>()
        where T : IRegistrable;
}