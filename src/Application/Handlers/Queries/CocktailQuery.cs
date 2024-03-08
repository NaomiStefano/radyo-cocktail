using Cocktail.Application.Models.Dtos;
using Cocktail.Application.Repositories;
using Cocktail.Application.Specifications;
using MediatR;

namespace Cocktail.Application.Handlers.Queries;

public record CocktailQuery : IRequest<IEnumerable<CocktailDto>>;

public class CocktailQueryHandler(IQueryProcessor<Domain.Aggregates.Cocktail> cocktailRepository) : IRequestHandler<CocktailQuery, IEnumerable<CocktailDto>>
{
    public async Task<IEnumerable<CocktailDto>> Handle(CocktailQuery request, CancellationToken cancellationToken)
    {
        return await cocktailRepository.ListAsync(new CocktailSpec()
            .WithIngredients()
            .WithStep(), cancellationToken);
    }
}
