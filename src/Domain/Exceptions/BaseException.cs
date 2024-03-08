namespace Cocktail.Domain.Exceptions;

public abstract class BaseException : Exception
{
    public string? Code { get; init; }

    protected BaseException( string? message, string? code ) : base( message )
    {
        Code = code;
    }

    protected BaseException( string? message, Exception? innerException, string? code ) :
        base( message, innerException )
    {
        Code = code;
    }
}