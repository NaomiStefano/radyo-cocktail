using FluentAssertions;

namespace Api.IntegrationTest.Handlers.Commands;

public class CocktailCreateCommand : CocktailIntegrationTests
{
    [Fact]
    public async Task ShouldHaveANewCocktail()
    {
        // Arrange
        var request = new Cocktail.Application.Handlers.Commands.CocktailCreateCommand("Long Island");
        
        // Act
        var result = await Mediator.Send(request);
        
        // Assert
        var cocktail = await CocktailRepository.GetAsync(new Cocktail.Domain.Specifications.CocktailSpec().ById(result.Id).WithSteps());
        cocktail.Should().NotBeNull();
        cocktail.Name.Should().Be(request.Name);
    }
}