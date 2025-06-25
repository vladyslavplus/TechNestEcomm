namespace TechNest.Application.DTOs.Categories;

public class UpdateCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}