namespace TechNest.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string Brand { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public int Stock { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public ICollection<ProductAttribute> Attributes { get; set; } = new List<ProductAttribute>();
    public ICollection<ProductRating> Ratings { get; set; } = new List<ProductRating>();
    public ICollection<ProductComment> Comments { get; set; } = new List<ProductComment>();
    public ICollection<FavoriteProduct> Favorites { get; set; } = new List<FavoriteProduct>();
}