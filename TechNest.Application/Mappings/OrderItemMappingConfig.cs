using Mapster;
using TechNest.Application.DTOs.OrderItems;
using TechNest.Domain.Entities;

namespace TechNest.Application.Mappings;

public class OrderItemMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OrderItem, OrderItemDto>()
            .Map(dest => dest.ProductName, src => src.Product.Name)
            .Map(dest => dest.ProductImageUrl, src => src.Product.ImageUrl);
    }
}