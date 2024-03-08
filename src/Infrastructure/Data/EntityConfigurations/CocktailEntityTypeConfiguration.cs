using Cocktail.Infrastructure.Configurations;

namespace Cocktail.Infrastructure.Data.EntityConfigurations;

public sealed class CocktailEntityTypeConfiguration : EntityTypeConfiguration<Domain.Aggregates.Cocktail, Guid>;