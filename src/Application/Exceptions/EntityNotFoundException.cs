using Cocktail.Domain.Exceptions;

namespace Cocktail.Application.Exceptions;

public interface IEntityNotFoundException { }
public class EntityNotFoundException<T>(string exception) : BaseException(exception, DefaultErrorMessage), IEntityNotFoundException
{
    private const string DefaultErrorMessage = "ENTITY_NOT_FOUND";
    private const string ErrorMessageWithTypeAndId = "Cannot find '{{Entity}}' where {{PropertyName}} equals {{PropertyValue}}.";

    public EntityNotFoundException( string propertyName, object propertyValue )
        : this( ErrorMessageWithTypeAndId.Replace( "{{Entity}}", typeof( T ).Name )
            .Replace( "{{PropertyName}}", propertyName )
            .Replace( "{{PropertyValue}}", propertyValue.ToString() ) ) { }
}