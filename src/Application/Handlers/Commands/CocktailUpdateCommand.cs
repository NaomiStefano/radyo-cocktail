using Cocktail.Application.Exceptions;
using Cocktail.Application.Models.Dtos;
using Cocktail.Application.Repositories;
using Cocktail.Domain.Specifications;
using Mapster;
using MediatR;

namespace Cocktail.Application.Handlers.Commands;

public record CocktailUpdateCommand(Guid Id, string Name) : IRequest<CocktailDto>;

public class CocktailUpdateCommandHandler(IAsyncRepository<Domain.Aggregates.Cocktail> cocktailRepository) : IRequestHandler<CocktailUpdateCommand, CocktailDto>
{
    public async Task<CocktailDto> Handle(CocktailUpdateCommand request, CancellationToken cancellationToken)
    {
        var cocktail = await cocktailRepository.GetAsync(new CocktailSpec().ById(request.Id), cancellationToken);
        if (cocktail is null)
            throw new EntityNotFoundException<Domain.Aggregates.Cocktail>(nameof(request.Id), request.Id);
        
        cocktail.UpdateName(request.Name);
        await cocktailRepository.UpdateAsync(cocktail, cancellationToken);
        return cocktail.Adapt<CocktailDto>();
    }
}