namespace TechNest.Application.DTOs.ProductComments;

public class UpdateProductCommentDto
{
    public Guid Id { get; set; }
    public string Text { get; set; } = null!;
}