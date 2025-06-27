using Mapster;
using TechNest.Application.DTOs.Orders;
using TechNest.Domain.Entities;

namespace TechNest.Application.Mappings;

public class OrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Order, OrderDto>()
            .Map(dest => dest.Status, src => src.Status.ToString());
        
        config.NewConfig<Order, UpdateOrderDto>()
            .Map(dest => dest.Status, src => src.Status.ToString());
    }
}