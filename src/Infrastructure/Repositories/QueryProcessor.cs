using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Cocktail.Application.Repositories;
using Cocktail.Domain;
using Cocktail.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cocktail.Infrastructure.Repositories;

public class QueryProcessor< T > : IQueryProcessor< T > where T : Entity
{
    private DbSet< T > DbSet => _context.Set< T >();
    private readonly CocktailContext _context;
    private readonly ISpecificationEvaluator _evaluator;

    public QueryProcessor( CocktailContext context)
    {
        _context = context ?? throw new ArgumentNullException( nameof( context ) );
        _evaluator = new SpecificationEvaluator();
    }

    public async Task< T > FindAsync( ISpecification< T > spec, CancellationToken cancellationToken = default )
    {
        return await ApplySpecification( spec ).FirstAsync( cancellationToken );
    }

    public async Task< TResult > FindAsync< TResult >( ISpecification< T, TResult > specification, CancellationToken cancellationToken = default )
    {
        return await ApplySpecification( specification ).FirstAsync( cancellationToken );
    }

    public async Task< T? > GetAsync( ISpecification< T > specification, CancellationToken cancellationToken = default )
    {
        return await ApplySpecification( specification ).FirstOrDefaultAsync( cancellationToken );
    }

    public async Task< TResult? > GetAsync< TResult >( ISpecification< T, TResult > specification, CancellationToken cancellationToken = default )
    {
        return await ApplySpecification( specification ).FirstOrDefaultAsync( cancellationToken );
    }

    public async Task< IReadOnlyList< T > > ListAsync( ISpecification< T > spec, CancellationToken cancellationToken = default )
    {
        return await ApplySpecification( spec ).ToListAsync( cancellationToken );
    }

    public async Task< IReadOnlyList< TResult > > ListAsync< TResult >( ISpecification< T, TResult > specification, CancellationToken cancellationToken = default )
    {
        return await ApplySpecification( specification ).ToListAsync( cancellationToken );
    }

    public IQueryable< TResult > List< TResult >( ISpecification< T, TResult > spec)
    {
        return ApplySpecification(spec);
    }

    public async Task< int > CountAsync( ISpecification< T > spec, CancellationToken cancellationToken = default )
    {
        return await ApplySpecification( spec ).CountAsync( cancellationToken );
    }

    protected IQueryable< T > ApplySpecification( ISpecification< T > specification, bool evaluateCriteriaOnly = false )
    {
        return _evaluator.GetQuery( DbSet.AsNoTracking(), specification, evaluateCriteriaOnly );
    }

    protected IQueryable< TResult > ApplySpecification< TResult >( ISpecification< T, TResult > specification )
    {
        if ( specification is null ) throw new ArgumentNullException( nameof( specification ), "Specification is required" );

        if ( specification.Selector is null ) throw new SelectorNotFoundException();
        
        return _evaluator.GetQuery( DbSet.AsNoTracking().AsQueryable(), specification );
    }
}