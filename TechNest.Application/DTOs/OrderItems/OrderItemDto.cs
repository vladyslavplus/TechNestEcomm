namespace TechNest.Application.DTOs.OrderItems;

public class OrderItemDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductImageUrl { get; set; } = null!;
}