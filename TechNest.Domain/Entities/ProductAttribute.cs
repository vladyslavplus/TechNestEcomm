namespace TechNest.Domain.Entities;

public class ProductAttribute
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
}