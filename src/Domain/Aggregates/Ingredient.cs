namespace Cocktail.Domain.Aggregates;

public class Ingredient : Entity<Guid>
{
    public string Name { get; private set; }
    public double AlcoholLevel { get; private set; }

    public Ingredient(string name, double alcoholLevel)
    {
        Name = name;
        AlcoholLevel = alcoholLevel;
        CreatedOn = DateTimeOffset.UtcNow;
    }
    
    public void SetAlcoholLevel(double alcoholLevel)
    {
        AlcoholLevel = alcoholLevel;
    }
    
    public void Remove()
    {
        DeletedOn = DateTimeOffset.UtcNow;
    }
}