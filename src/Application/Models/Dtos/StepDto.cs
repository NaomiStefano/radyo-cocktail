using Cocktail.Application.Mappers;
using Cocktail.Domain.Aggregates;

namespace Cocktail.Application.Models.Dtos;

public class StepDto : IMapFrom<Step>
{
    public int Order { get; set; }
    public string Description { get; set; } = default!;
}