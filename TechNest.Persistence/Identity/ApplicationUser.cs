using Microsoft.AspNetCore.Identity;
using TechNest.Domain.Entities;

namespace TechNest.Persistence.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FullName { get; set; } = null!;
    public string? City { get; set; }
    public string? Phone { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public ICollection<FavoriteProduct> Favorites { get; set; } = new List<FavoriteProduct>();
    public ICollection<ProductRating> Ratings { get; set; } = new List<ProductRating>();
    public ICollection<ProductComment> Comments { get; set; } = new List<ProductComment>();
}