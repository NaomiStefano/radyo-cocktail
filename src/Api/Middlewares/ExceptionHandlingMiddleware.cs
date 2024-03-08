
using System.Net;
using System.Text.Json;
using Cocktail.Api.Extensions;
using Cocktail.Application.Exceptions;
using Cocktail.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ApplicationException = Cocktail.Application.Exceptions.ApplicationException;

namespace Cocktail.Api.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger< ExceptionHandlingMiddleware > logger)
{
    public async Task Invoke( HttpContext httpContext )
    {
        try
        {
            await next( httpContext );
        }
        catch ( Exception exception )
        {
            await WriteResponse( httpContext, exception );
        }
    }

    private async Task WriteResponse( HttpContext httpContext, Exception exception )
    {
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        UpdateResponse( httpContext, exception );
        await httpContext.Response.WriteAsync(
            JsonSerializer.Serialize(ToProblemDetails(exception, httpContext.Response.StatusCode), serializeOptions));
    }

    private void UpdateResponse( HttpContext httpContext, Exception exception )
    {
        // https://datatracker.ietf.org/doc/html/rfc7807
        httpContext.Response.ContentType = "application/problem+json";
        httpContext.Response.StatusCode = GetHttpStatusCodeFromException( exception );
    }

    protected virtual int GetHttpStatusCodeFromException( Exception exception )
    {
        var code = exception switch
        {
            IEntityNotFoundException => HttpStatusCode.NotFound,
            ApplicationException => HttpStatusCode.InternalServerError,
            ValidationException => HttpStatusCode.BadRequest,
            InvalidOperationException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };
        return ( int )code;
    }

    protected virtual ProblemDetails ToProblemDetails( Exception exception, int statusCode )
    {
        switch ( exception )
        {
            case BaseException serviceException:
                logger.LogError( serviceException,
                                  "Exception of type {ExceptionType} raised with message '{ExceptionMessage}' (code = '{ExceptionCode}')",
                                  serviceException.GetType(), serviceException.Message, serviceException.Code );
                return serviceException.AsProblem( statusCode );
            case ValidationException validationException:
                logger.LogError( validationException,
                                  "Exception of type {ExceptionType} raised with message '{ExceptionMessage}'", validationException.GetType(),
                                  validationException.Message );
                return validationException.AsProblem( statusCode );
            default:
                logger.LogError( exception,
                                  "Exception of type {ExceptionType} raised with message '{ExceptionMessage}'", exception.GetType(),
                                  exception.Message );
                return exception.AsProblem( statusCode );
        }
    }
}