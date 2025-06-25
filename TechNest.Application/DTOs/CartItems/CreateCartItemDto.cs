namespace TechNest.Application.DTOs.CartItems;

public class CreateCartItemDto
{
    public Guid? UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}