using Cocktail.Domain.Aggregates;
using FluentAssertions;

namespace Api.IntegrationTest.Handlers.Queries;

public class IngredientQuery : CocktailIntegrationTests
{
    [Fact]
    public async Task ShouldHave2Ingredients()
    {
        // Arrange
        var rum = new Ingredient("Rum", 40);
        var vermouth = new Ingredient("Vermouth", 16);
        
        await IngredientRepository.AddAsync(rum);
        await IngredientRepository.AddAsync(vermouth);
        
        // Act
        var ingredients = await Mediator.Send(new Cocktail.Application.Handlers.Queries.IngredientQuery());
      
        // Assert
        ingredients.Should().HaveCount(2);
        ingredients.First().Name.Should().Be(rum.Name);
        ingredients.First().AlcoholLevel.Should().Be(rum.AlcoholLevel);
        ingredients.First().Power.Should().Be("High");
        ingredients.Last().Name.Should().Be(vermouth.Name);
        ingredients.Last().AlcoholLevel.Should().Be(vermouth.AlcoholLevel);
        ingredients.Last().Power.Should().Be("Sweet");
    }
}