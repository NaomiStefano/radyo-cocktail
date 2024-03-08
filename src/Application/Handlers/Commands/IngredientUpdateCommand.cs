using Cocktail.Application.Exceptions;
using Cocktail.Application.Models.Dtos;
using Cocktail.Application.Repositories;
using Cocktail.Domain.Aggregates;
using Cocktail.Domain.Specifications;
using FluentValidation;
using Mapster;
using MediatR;

namespace Cocktail.Application.Handlers.Commands;

public record IngredientUpdateCommand(Guid Id, double AlcoholLevel) : IRequest<IngredientDto>;

public class IngredientUpdateValidator : AbstractValidator<IngredientUpdateCommand>
{
    public IngredientUpdateValidator()
    {
        RuleFor(x => x.AlcoholLevel)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .LessThan(100);
    }
}

public class IngredientUpdateCommandHandler(IAsyncRepository<Ingredient> ingredientRepository) : IRequestHandler<IngredientUpdateCommand, IngredientDto>
{
    public async Task<IngredientDto> Handle(IngredientUpdateCommand request, CancellationToken cancellationToken)
    {
        var ingredient = await ingredientRepository.GetAsync(new IngredientSpec().ById(request.Id), cancellationToken);
        if (ingredient is null)
            throw new EntityNotFoundException<Ingredient>(nameof(request.Id), request.Id);

        ingredient.SetAlcoholLevel(request.AlcoholLevel);
        await ingredientRepository.UpdateAsync(ingredient, cancellationToken);
        return ingredient.Adapt<IngredientDto>();
    }
}