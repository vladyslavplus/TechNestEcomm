namespace TechNest.Application.DTOs.ProductRatings;

public class ProductRatingDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;

    public Guid UserId { get; set; }
    public string UserFullName { get; set; } = null!;

    public int Value { get; set; }
    public DateTime RatedAt { get; set; }
}