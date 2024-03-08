using Ardalis.Specification;
using Cocktail.Application.Models.Dtos;
using Cocktail.Domain.Aggregates;
using Mapster;

namespace Cocktail.Application.Specifications;

public sealed class IngredientSpec : Specification<Ingredient, IngredientDto>
{
    public IngredientSpec()
    {
        Query.Select(i => i.Adapt<IngredientDto>());
    }
}