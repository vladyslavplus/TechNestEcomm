namespace TechNest.Application.DTOs.Orders;

public class CreateOrderOrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}