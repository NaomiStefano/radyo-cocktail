namespace Cocktail.Domain;

public abstract class Entity { }

public abstract class Entity< TKeyType > : Entity
{
    public TKeyType Id { get; set; } = default!;
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? LastModifiedOn { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
}