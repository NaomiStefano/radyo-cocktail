using Ardalis.Specification;
using Cocktail.Domain.Aggregates;

namespace Cocktail.Domain.Specifications;

public sealed class IngredientSpec : Specification<Ingredient>
{
    public IngredientSpec ById(Guid id)
    {
        Query.Where(i => i.Id == id);
        return this;
    }
}