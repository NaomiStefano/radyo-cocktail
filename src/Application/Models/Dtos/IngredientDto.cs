using Cocktail.Domain.Aggregates;
using Mapster;

namespace Cocktail.Application.Models.Dtos;

public class IngredientDto : IMapFrom<Ingredient>
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public double AlcoholLevel { get; set; }
    public string? Power { get; set; }

    public void Mapping(TypeAdapterConfig config)
    {
        config.ForType<Ingredient, IngredientDto>()
            .Map(dest => dest.Power, 
                src => src.AlcoholLevel > 18.0 ? "High" : src.AlcoholLevel == 0 ? "Soft" : "Sweet");
    }
}