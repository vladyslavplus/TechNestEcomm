namespace TechNest.Application.DTOs.OrderItems;

public class UpdateOrderItemDto
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}