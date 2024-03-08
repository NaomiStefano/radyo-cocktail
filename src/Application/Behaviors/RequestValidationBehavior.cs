using FluentValidation;
using MediatR;
using ValidationException = FluentValidation.ValidationException;

namespace Cocktail.Application.Behaviors;

public class RequestValidationBehavior<TRequest, TResponse>
    (IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var failures = validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();
        if (failures.Count != 0)
        {
            throw new ValidationException("A model validation error occurred" , failures);
        }
        return next();
    }
}