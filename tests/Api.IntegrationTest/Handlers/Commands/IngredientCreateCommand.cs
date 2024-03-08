using FluentAssertions;

namespace Api.IntegrationTest.Handlers.Commands;

public class IngredientCreateCommand : CocktailIntegrationTests
{
    [Fact]
    public async Task ShouldHaveANewIngredient()
    {
        // Arrange
        var request = new Cocktail.Application.Handlers.Commands.IngredientCreateCommand("Vodka", 40);
        
        // Act
        var result = await Mediator.Send(request);
        
        // Assert
        var ingredient = await IngredientRepository.GetAsync(new Cocktail.Domain.Specifications.IngredientSpec().ById(result.Id));
        ingredient.Should().NotBeNull();
        ingredient.Name.Should().Be(request.Name);
        ingredient.AlcoholLevel.Should().Be(request.AlcoholLevel);
    }
}