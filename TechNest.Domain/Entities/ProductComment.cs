using System.ComponentModel.DataAnnotations.Schema;

namespace TechNest.Domain.Entities;

public class ProductComment
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    
    public Guid UserId { get; set; }
    [NotMapped]
    public User User { get; set; } = null!; 

    public string Text { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}