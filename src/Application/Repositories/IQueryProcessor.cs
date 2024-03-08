using Ardalis.Specification;
using Cocktail.Domain;

namespace Cocktail.Application.Repositories;

public interface IQueryProcessor< T > where T : Entity
{
    Task<T> FindAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
    Task<TResult> FindAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);
    Task<T?> GetAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    Task<TResult?> GetAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);
    IQueryable< TResult > List< TResult >( ISpecification< T, TResult > spec);
    Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
}