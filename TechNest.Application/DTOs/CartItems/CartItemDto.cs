namespace TechNest.Application.DTOs.CartItems;

public class CartItemDto
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime AddedAt { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public string ProductImageUrl { get; set; } = null!;
}