using Cocktail.Application.Exceptions;
using Cocktail.Application.Repositories;
using Cocktail.Domain.Aggregates;
using Cocktail.Domain.Specifications;
using FluentValidation;
using MediatR;
using ApplicationException = Cocktail.Application.Exceptions.ApplicationException;
using CocktailSpec = Cocktail.Domain.Specifications.CocktailSpec;
using Unit = Cocktail.Domain.Aggregates.Unit;

namespace Cocktail.Application.Handlers.Commands;

public record CocktailAddIngredientCommand(Guid Id, Guid IngredientId, double Quantity, string Unit) : IRequest;

public class CocktailAddIngredientValidator : AbstractValidator<CocktailAddIngredientCommand>
{
    public CocktailAddIngredientValidator()
    {
        RuleFor(x => x.Unit)
            .NotNull()
            .NotEmpty()
            .Must(x => Enum.TryParse<Unit>(x, true, out _))
            .WithMessage("Invalid unit");
    }
}

public class CocktailAddIngredientCommandHandler(IQueryProcessor<Ingredient> ingredientRepository, IAsyncRepository<Domain.Aggregates.Cocktail> cocktailRepository) : IRequestHandler<CocktailAddIngredientCommand>
{
    public async Task Handle(CocktailAddIngredientCommand request, CancellationToken cancellationToken)
    {
        var cocktail = await cocktailRepository.GetAsync(new CocktailSpec().ById(request.Id).WithIngredients(), cancellationToken);
        if (cocktail is null)
            throw new EntityNotFoundException<Domain.Aggregates.Cocktail>(nameof(request.Id), request.Id);

        var ingredient = await ingredientRepository.GetAsync(new IngredientSpec().ById(request.IngredientId), cancellationToken);
        if (ingredient is null)
            throw new EntityNotFoundException<Ingredient>(nameof(request.IngredientId), request.IngredientId);
        
        if (cocktail.Compositions.Any(c => c.IngredientId == request.IngredientId))
            throw new ApplicationException(CocktailErrorCodes.IngredientAlreadyExists);

        var unit = Enum.Parse<Unit>(request.Unit, true);
        cocktail.AddComposition(new Composition(request.Quantity, unit, ingredient));
        await cocktailRepository.UpdateAsync(cocktail, cancellationToken);
    }
}