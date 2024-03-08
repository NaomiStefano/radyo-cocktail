using Cocktail.Application.Exceptions;
using Cocktail.Application.Repositories;
using Cocktail.Domain.Aggregates;
using MediatR;
using CocktailSpec = Cocktail.Domain.Specifications.CocktailSpec;

namespace Cocktail.Application.Handlers.Commands;

public record CocktailRemoveIngredientCommand(Guid Id, Guid IngredientId) : IRequest;

public class CocktailRemoveIngredientCommandHandler(IAsyncRepository<Domain.Aggregates.Cocktail> cocktailRepository) : IRequestHandler<CocktailRemoveIngredientCommand>
{
    public async Task Handle(CocktailRemoveIngredientCommand request, CancellationToken cancellationToken)
    {
        var cocktail = await cocktailRepository.GetAsync(new CocktailSpec().ById(request.Id).WithIngredients(), cancellationToken);
        if (cocktail is null)
            throw new EntityNotFoundException<Domain.Aggregates.Cocktail>(nameof(request.Id), request.Id);

        var composition = cocktail.Compositions.FirstOrDefault(c => c.IngredientId == request.IngredientId);
        if (composition is null)
            throw new EntityNotFoundException<Ingredient>(nameof(request.IngredientId), request.IngredientId);

        cocktail.RemoveComposition(composition);
        await cocktailRepository.UpdateAsync(cocktail, cancellationToken);
    }
}