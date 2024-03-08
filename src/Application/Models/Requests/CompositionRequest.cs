namespace Cocktail.Application.Models.Requests;

public class CompositionRequest
{
    public Guid IngredientId { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; } = default!;
}