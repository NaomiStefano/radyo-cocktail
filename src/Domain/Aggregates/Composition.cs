namespace Cocktail.Domain.Aggregates;

public class Composition : Entity<Guid>
{
    public double Quantity { get; private set; }
    public Unit Unit { get; private set; }
    public Ingredient Ingredient { get; private set; }
    public Guid IngredientId { get; private set; }

    private Composition()
    {
        CreatedOn = DateTimeOffset.UtcNow;
    }
    public Composition(double quantity, Unit unit, Ingredient ingredient) : this()
    {
        Quantity = quantity;
        Unit = unit;
        Ingredient = ingredient;
    }
}