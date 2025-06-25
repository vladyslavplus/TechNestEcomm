using Mapster;
using TechNest.Application.DTOs.CartItems;
using TechNest.Domain.Entities;

namespace TechNest.Application.Mappings;

public class CartItemMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CartItem, CartItemDto>()
            .Map(dest => dest.ProductName, src => src.Product.Name)
            .Map(dest => dest.ProductPrice, src => src.Product.Price)
            .Map(dest => dest.ProductImageUrl, src => src.Product.ImageUrl);
    }
}