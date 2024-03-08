using Cocktail.Application.Mappers;
using Cocktail.Domain.Aggregates;

namespace Cocktail.Application.Models.Dtos;

public class CompositionDto : IMapFrom<Composition>
{
    public double Quantity { get; set; }
    public Unit Unit { get; set; }
    public IngredientDto Ingredient { get; set; } = default!;
}