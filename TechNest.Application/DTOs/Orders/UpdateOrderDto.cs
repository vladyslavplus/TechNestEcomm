using TechNest.Domain.Entities.Enums;

namespace TechNest.Application.DTOs.Orders;

public class UpdateOrderDto
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
}