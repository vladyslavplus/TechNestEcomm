using Mapster;
using TechNest.Application.DTOs.ProductRatings;
using TechNest.Domain.Entities;

namespace TechNest.Application.Mappings;

public class ProductRatingMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProductRating, ProductRatingDto>()
            .Map(dest => dest.ProductName, src => src.Product.Name)
            .Map(dest => dest.UserFullName, src => src.User.FullName);
    }
}