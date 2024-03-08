using Cocktail.Application.Exceptions;
using Cocktail.Application.Repositories;
using Cocktail.Domain.Specifications;
using MediatR;

namespace Cocktail.Application.Handlers.Commands;

public record CocktailDeleteCommand(Guid Id) : IRequest;

public class CocktailDeleteCommandHandler(IAsyncRepository<Domain.Aggregates.Cocktail> cocktailRepository) : IRequestHandler<CocktailDeleteCommand>
{
    public async Task Handle(CocktailDeleteCommand request, CancellationToken cancellationToken)
    {
        var cocktail = await cocktailRepository.GetAsync(new CocktailSpec().ById(request.Id), cancellationToken);
        if (cocktail is null)
            throw new EntityNotFoundException<Domain.Aggregates.Cocktail>(nameof(request.Id), request.Id);
        
        cocktail.Remove();
        await cocktailRepository.UpdateAsync(cocktail, cancellationToken);
    }
}