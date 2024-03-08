using Cocktail.Application.Models.Dtos;
using Cocktail.Application.Repositories;
using Cocktail.Domain.Aggregates;
using FluentValidation;
using Mapster;
using MediatR;

namespace Cocktail.Application.Handlers.Commands;

public record IngredientCreateCommand(string Name, double AlcoholLevel) : IRequest<IngredientDto>;

public class IngredientCreateValidator : AbstractValidator<IngredientCreateCommand>
{
    public IngredientCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.AlcoholLevel)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .LessThan(100);
    }
}

public class IngredientCreateCommandHandler(IAsyncRepository<Ingredient> ingredientRepository) : IRequestHandler<IngredientCreateCommand, IngredientDto>
{
    public async Task<IngredientDto> Handle(IngredientCreateCommand request, CancellationToken cancellationToken)
    {
        var ingredient = new Ingredient(request.Name, request.AlcoholLevel);
        await ingredientRepository.AddAsync(ingredient, cancellationToken);
        return ingredient.Adapt<IngredientDto>();
    }
}