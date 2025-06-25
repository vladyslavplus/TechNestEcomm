namespace TechNest.Application.DTOs.ProductAttributes;

public class UpdateProductAttributeDto
{
    public Guid Id { get; set; }
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
}