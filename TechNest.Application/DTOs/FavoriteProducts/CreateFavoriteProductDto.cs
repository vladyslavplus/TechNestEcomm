namespace TechNest.Application.DTOs.FavoriteProducts;

public class CreateFavoriteProductDto
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
}