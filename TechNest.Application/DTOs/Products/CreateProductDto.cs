namespace TechNest.Application.DTOs.Products;

public class CreateProductDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string Brand { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public int Stock { get; set; }
    public Guid CategoryId { get; set; }
}