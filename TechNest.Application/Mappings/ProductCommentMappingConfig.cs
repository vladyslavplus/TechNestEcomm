using Mapster;
using TechNest.Application.DTOs.ProductComments;
using TechNest.Domain.Entities;

namespace TechNest.Application.Mappings;

public class ProductCommentMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProductComment, ProductCommentDto>()
            .Map(dest => dest.UserFullName, src => src.User.FullName)
            .Map(dest => dest.UserCity, src => src.User.City)
            .Map(dest => dest.ProductName, src => src.Product.Name);
    }
}