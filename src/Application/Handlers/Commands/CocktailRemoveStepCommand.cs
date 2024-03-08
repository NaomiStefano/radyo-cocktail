using Cocktail.Application.Exceptions;
using Cocktail.Application.Repositories;
using Cocktail.Domain.Aggregates;
using Cocktail.Domain.Specifications;
using MediatR;

namespace Cocktail.Application.Handlers.Commands;

public record CocktailRemoveStepCommand(Guid Id, Guid StepId) : IRequest;

public class CocktailRemoveStepCommandHandler(IAsyncRepository<Domain.Aggregates.Cocktail> cocktailRepository) : IRequestHandler<CocktailRemoveStepCommand>
{
    public async Task Handle(CocktailRemoveStepCommand request, CancellationToken cancellationToken)
    {
        var cocktail = await cocktailRepository.GetAsync(new CocktailSpec().ById(request.Id).WithSteps(), cancellationToken);
        if (cocktail is null)
            throw new EntityNotFoundException<Domain.Aggregates.Cocktail>(nameof(request.Id), request.Id);
        
        var stepToRemove = cocktail.Steps.FirstOrDefault(s => s.Id == request.StepId);
        if (stepToRemove is null)
            throw new EntityNotFoundException<Step>(nameof(request.StepId), request.StepId);
        
        cocktail.RemoveStep(stepToRemove);
        await cocktailRepository.UpdateAsync(cocktail, cancellationToken);
    }
}