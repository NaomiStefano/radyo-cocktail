using Cocktail.Domain;
using Mapster;

namespace Cocktail.Application.Mappers;

public interface IMapFrom< TEntity >  where TEntity : Entity
{
    void Mapping(TypeAdapterConfig config)
    {
        config.NewConfig(typeof(TEntity), GetType());
    }
}