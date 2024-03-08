using Ardalis.Specification;
using Cocktail.Domain;

namespace Cocktail.Application.Repositories;

public interface IAsyncRepository< T > where T : Entity
{
    Task<T?> GetAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
    Task<T> FindAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
    Task<List<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateRangeAsync(IEnumerable<T> entity, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task<int> DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
}