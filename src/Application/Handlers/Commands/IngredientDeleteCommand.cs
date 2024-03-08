using Cocktail.Application.Exceptions;
using Cocktail.Application.Repositories;
using Cocktail.Domain.Aggregates;
using Cocktail.Domain.Specifications;
using MediatR;
using ApplicationException = Cocktail.Application.Exceptions.ApplicationException;

namespace Cocktail.Application.Handlers.Commands;

public record IngredientDeleteCommand(Guid Id) : IRequest;

public class IngredientDeleteCommandHandler(IQueryProcessor<Domain.Aggregates.Cocktail> cocktailRepository, IAsyncRepository<Ingredient> ingredientRepository) : IRequestHandler<IngredientDeleteCommand>
{
    public async Task Handle(IngredientDeleteCommand request, CancellationToken cancellationToken)
    {
        var ingredient = await ingredientRepository.GetAsync(new IngredientSpec().ById(request.Id), cancellationToken);
        if (ingredient is null)
            throw new EntityNotFoundException<Ingredient>(nameof(request.Id), request.Id);

        var cocktail = await cocktailRepository.GetAsync(new CocktailSpec().HasIngredient(ingredient), cancellationToken);
        if (cocktail is not null)
            throw new ApplicationException(CocktailErrorCodes.IngredientUsed);

        ingredient.Remove();
        await ingredientRepository.UpdateAsync(ingredient, cancellationToken);
    }
}