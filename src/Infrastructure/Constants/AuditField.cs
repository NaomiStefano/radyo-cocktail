using Cocktail.Domain;

namespace Cocktail.Infrastructure.Constants;

public static class AuditField
{
    public const string CreatedOn = nameof( Entity< int >.CreatedOn );
    public const string LastModifiedOn = nameof( Entity< int >.LastModifiedOn );
    public const string DeletedOn = nameof( Entity< int >.DeletedOn );
}