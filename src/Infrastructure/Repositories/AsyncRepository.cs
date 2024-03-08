using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Cocktail.Application.Repositories;
using Cocktail.Domain;
using Cocktail.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cocktail.Infrastructure.Repositories;

public class AsyncRepository<T>(CocktailContext context) : IAsyncRepository<T> where T : Entity
{
    private DbSet< T > DbSet => _context.Set< T >();
    private readonly DbContext _context = context ?? throw new ArgumentNullException( nameof( context ) );
    private readonly ISpecificationEvaluator _specificationEvaluator = new SpecificationEvaluator();

    public async Task< T? > GetAsync( ISpecification< T > spec, CancellationToken cancellationToken = default )
    {
        return await ApplySpecification( spec ).FirstOrDefaultAsync( cancellationToken );
    }

    public async Task< T > FindAsync( ISpecification< T > spec, CancellationToken cancellationToken = default )
    {
        return await ApplySpecification( spec ).FirstAsync( cancellationToken );
    }

    public async Task< List< T > > ListAsync( ISpecification< T > spec, CancellationToken cancellationToken = default )
    {
        return await ApplySpecification( spec ).ToListAsync( cancellationToken );
    }

    public async Task AddRangeAsync( IEnumerable< T > entities, CancellationToken cancellationToken = default )
    {
        await DbSet.AddRangeAsync( entities, cancellationToken );
        await _context.SaveChangesAsync( cancellationToken );
    }

    public virtual async Task< T > AddAsync( T entity, CancellationToken cancellationToken = default )
    {
        DbSet.Add( entity );
        await _context.SaveChangesAsync( cancellationToken );
        return entity;
    }

    public virtual async Task UpdateAsync( T entity, CancellationToken cancellationToken = default )
    {
        _context.Entry( entity ).State = EntityState.Modified;
        await _context.SaveChangesAsync( cancellationToken );
    }

    public virtual async Task UpdateRangeAsync( IEnumerable< T > entity, CancellationToken cancellationToken = default )
    {
        _context.UpdateRange( entity );
        await _context.SaveChangesAsync( cancellationToken );
    }

    public virtual async Task< int > DeleteAsync( T entity, CancellationToken cancellationToken = default )
    {
        DbSet.Remove( entity );
        return await _context.SaveChangesAsync( cancellationToken );
    }

    public async Task< int > DeleteRangeAsync( IEnumerable< T > entities, CancellationToken cancellationToken = default )
    {
        DbSet.RemoveRange( entities );
        return await _context.SaveChangesAsync( cancellationToken );
    }

    protected IQueryable< T > ApplySpecification( ISpecification< T > specification )
    {
        return _specificationEvaluator.GetQuery( DbSet.AsQueryable(), specification );
    }
}