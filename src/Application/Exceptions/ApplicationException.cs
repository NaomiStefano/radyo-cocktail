using Cocktail.Domain.Exceptions;

namespace Cocktail.Application.Exceptions;

public class ApplicationException : BaseException
{
    public ApplicationException(Enum errorCode) : base(CocktailErrorDictionary.Instance[errorCode], errorCode.ToString()) { }
    public ApplicationException( string? message, string? errorCode = null ) : base( message, errorCode) { }
    public ApplicationException( string? message, Exception? innerException, string errorCode ) : base( message, innerException, errorCode ) { }
}