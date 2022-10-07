using System;
using EntitySystem.Shared.Exceptions;
using NHibernate;

namespace EntitySystem.Server.Services;

public sealed class DatabaseService : IDisposable
{
    private readonly ISessionFactory _sessionFactory;

    private ISession _session;

    private ITransaction _transaction;

    public DatabaseService(ISessionFactory sessionFactory)
    {
        _sessionFactory = sessionFactory;
    }

    public ISession OpenSession()
    {
        if (_session != null) throw new GeneralFriendlyException("Cannot open session, session already opened.");

        return _session = _sessionFactory.OpenSession();
    }

    public ISession GetSession()
    {
        if (_session == null) throw new GeneralFriendlyException("Cannot get session, open session first.");

        return _session;
    }

    public ITransaction BeginTransaction()
    {
        if (_session == null) throw new GeneralFriendlyException("Cannot begin transaction, open session first.");

        if (_transaction != null) throw new GeneralFriendlyException("Cannot begin transaction, transaction already began.");

        return _transaction = _session.BeginTransaction();
    }

    public async void CommitTransaction()
    {
        if (_transaction == null) throw new GeneralFriendlyException("Cannot commit transaction, begin transaction first.");

        if (_transaction.WasCommitted) throw new GeneralFriendlyException("Cannot commit transaction, transaction rolled back.");

        if (_transaction.WasRolledBack) throw new GeneralFriendlyException("Cannot commit transaction, transaction already committed.");

        await _transaction.CommitAsync();
    }

    public async void RollbackTransaction()
    {
        if (_transaction == null) throw new GeneralFriendlyException("No transaction for rollback.");

        if (_transaction.WasCommitted) throw new GeneralFriendlyException("Cannot rollback, transaction committed.");

        if (_transaction.WasRolledBack) throw new GeneralFriendlyException("Transaction already rolled back.");

        await _transaction.RollbackAsync();
    }

    public void Dispose()
    {
        _session?.Dispose();

        _transaction?.Dispose();
    }
}