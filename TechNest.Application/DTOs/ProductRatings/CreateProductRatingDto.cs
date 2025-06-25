namespace TechNest.Application.DTOs.ProductRatings;

public class CreateProductRatingDto
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public int Value { get; set; }
}