using Cocktail.Domain.Aggregates;
using Cocktail.Infrastructure.Configurations;

namespace Cocktail.Infrastructure.Data.EntityConfigurations;

public sealed class IngredientEntityTypeConfiguration : EntityTypeConfiguration<Ingredient, Guid>;
