using Mapster;
using TechNest.Application.DTOs.Products;
using TechNest.Domain.Entities;

namespace TechNest.Application.Mappings;

public class ProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductDto>()
            .Map(dest => dest.CategoryName, src => src.Category.Name)
            .Map(dest => dest.AverageRating, src => src.Ratings.Count != 0 ? src.Ratings.Average(r => r.Value) : 0)
            .Map(dest => dest.CommentCount, src => src.Comments.Count)
            .Map(dest => dest.FavoriteCount, src => src.Favorites.Count)
            .Map(dest => dest.Attributes, src => src.Attributes);
    }
}