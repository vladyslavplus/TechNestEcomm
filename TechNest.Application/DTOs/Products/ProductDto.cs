using TechNest.Application.DTOs.ProductAttributes;

namespace TechNest.Application.DTOs.Products;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string Brand { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public int Stock { get; set; }

    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;

    public double AverageRating { get; set; }
    public int CommentCount { get; set; }
    public int FavoriteCount { get; set; }

    public List<ProductAttributeDto> Attributes { get; set; } = [];
}