namespace TechNest.Application.DTOs.ProductAttributes;

public class ProductAttributeDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
}