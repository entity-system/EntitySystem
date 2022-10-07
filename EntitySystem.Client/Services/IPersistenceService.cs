using System.Threading.Tasks;

namespace EntitySystem.Client.Services;

public interface IPersistenceService
{
    Task SaveAsync<T>(T entity)
        where T : class;

    Task<T> GetAsync<T>();
    Task DeleteAsync<T>(T _);
}