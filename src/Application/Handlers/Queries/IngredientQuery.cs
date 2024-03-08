using Cocktail.Application.Models.Dtos;
using Cocktail.Application.Repositories;
using Cocktail.Application.Specifications;
using Cocktail.Domain.Aggregates;
using MediatR;

namespace Cocktail.Application.Handlers.Queries;

public record IngredientQuery : IRequest<IEnumerable<IngredientDto>>;

public class IngredientQueryHandler(IQueryProcessor<Ingredient> ingredientRepository) : IRequestHandler<IngredientQuery, IEnumerable<IngredientDto>>
{
    public async Task<IEnumerable<IngredientDto>> Handle(IngredientQuery request, CancellationToken cancellationToken)
    {
        return await ingredientRepository.ListAsync(new IngredientSpec(), cancellationToken);
    }
}
