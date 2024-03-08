using Cocktail.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Cocktail.Api.Extensions;

public static class ProblemDetailsExtensions
{
    public static ValidationProblemDetails AsProblem( this ValidationException exception, int statusCode )
    {
        var errors = exception.Errors.ToDictionary( validationFailure => validationFailure.PropertyName,
                                                    validationFailure => new[] { validationFailure.ErrorMessage } );
        return new ValidationProblemDetails( errors )
        {
            Title = "Request invalid (validation errors)",
            Status = statusCode,
            Detail = exception.Message
        };
    }

    public static ProblemDetails AsProblem( this BaseException exception, int statusCode )
    {
        return new ProblemDetails
        {
            Title = "An error occurred",
            Status = statusCode,
            Detail = exception.Message,
            Extensions =
            {
                new KeyValuePair< string, object? >( "ErrorCode", exception.Code ?? "Unknown" )
            }
        };
    }

    public static ProblemDetails AsProblem( this Exception exception, int statusCode )
    {
        return new ProblemDetails
        {
            Title = "An unknown error occurred",
            Status = statusCode,
            Detail = exception.Message
        };
    }
}