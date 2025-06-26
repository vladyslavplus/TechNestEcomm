using System.ComponentModel.DataAnnotations.Schema;

namespace TechNest.Domain.Entities;

public class ProductRating
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public Guid UserId { get; set; }
    [NotMapped]
    public User User { get; set; } = null!;
    public int Value { get; set; } 
    public DateTime RatedAt { get; set; } = DateTime.UtcNow;
}