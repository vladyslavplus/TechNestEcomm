namespace TechNest.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string? City { get; set; }
    public string? Phone { get; set; }
    public string Email { get; set; } = null!;
}