using NHibernate;

namespace EntitySystem.Server.Services;

    public interface ISessionService
    {
        ISession GetSession();
    }