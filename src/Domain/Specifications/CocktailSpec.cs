using Ardalis.Specification;
using Cocktail.Domain.Aggregates;

namespace Cocktail.Domain.Specifications;

public sealed class CocktailSpec : Specification<Aggregates.Cocktail>
{
    public CocktailSpec ById(Guid id)
    {
        Query.Where(i => i.Id == id);
        return this;
    }
    
    public CocktailSpec HasIngredient(Ingredient ingredient)
    {
        Query.Where(i => i.Compositions.Any(c => c.IngredientId == ingredient.Id));
        return this;
    }
    
    public CocktailSpec WithSteps()
    {
        Query.Include(c => c.Steps);
        return this;
    }
    
    public CocktailSpec WithIngredients()
    {
        Query.Include(c => c.Compositions).ThenInclude(c => c.Ingredient);
        return this;
    }
}