namespace Cocktail.Application.Exceptions;

public static class CocktailErrorDictionary
{
    public static readonly Dictionary<Enum, string> Instance = new()
    {
        { CocktailErrorCodes.EntityNotFound, "Entity not found" },
        { CocktailErrorCodes.IngredientUsed, "The ingredient is used in a cocktail and cannot be deleted." },
        { CocktailErrorCodes.IngredientAlreadyExists, "The ingredient already exists."}
    };
}