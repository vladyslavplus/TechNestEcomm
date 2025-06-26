namespace TechNest.Application.DTOs.FavoriteProducts;

public class FavoriteProductDto
{
    public Guid UserId { get; set; }
    public FavoriteProductProductDto Product { get; set; } = null!;
}