using System.Linq.Expressions;

namespace GameStore.Repositories;

public interface IRepositorie<T> where T : class
{
    Task<List<T>> GetAllAsync(string? includeProperties = null);
    Task AddAsync(T entity);
    Task DeleteAsync(int id);
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate, string? includeProperties = null);
    Task SaveAsync();
}