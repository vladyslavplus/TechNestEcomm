namespace TechNest.Application.DTOs.ProductComments;

public class ProductCommentDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string UserFullName { get; set; } = null!;
    public string? UserCity { get; set; }
    public string ProductName { get; set; } = null!;
}