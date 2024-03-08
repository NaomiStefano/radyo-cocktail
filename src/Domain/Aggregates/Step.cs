namespace Cocktail.Domain.Aggregates;

public class Step : Entity<Guid>
{
    public int Order { get; private set; }
    public string Description { get; private set; }
    
    public Step(int order, string description)
    {
        Order = order;
        Description = description;
        CreatedOn = DateTimeOffset.UtcNow;
    }

    public void SetOrder(int order)
    {
        Order = order;
    }
}