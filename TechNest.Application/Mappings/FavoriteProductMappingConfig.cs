using Mapster;
using TechNest.Application.DTOs.FavoriteProducts;
using TechNest.Domain.Entities;

namespace TechNest.Application.Mappings;

public class FavoriteProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FavoriteProduct, FavoriteProductDto>()
            .Map(dest => dest.Product, src => src.Product);

        config.NewConfig<Product, FavoriteProductProductDto>()
            .Map(dest => dest.CategoryName, src => src.Category.Name);
    }
}