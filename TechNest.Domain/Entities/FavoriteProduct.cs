namespace TechNest.Domain.Entities;

public class FavoriteProduct
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
}