namespace TechNest.Application.DTOs.ProductComments;

public class CreateProductCommentDto
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; } = null!;
}