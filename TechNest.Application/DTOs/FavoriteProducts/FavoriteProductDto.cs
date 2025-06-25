namespace TechNest.Application.DTOs.FavoriteProducts;

public class FavoriteProductDto
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public FavoriteProductProductDto Product { get; set; } = null!;
}