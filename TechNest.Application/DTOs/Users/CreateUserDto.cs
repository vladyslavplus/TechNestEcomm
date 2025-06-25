namespace TechNest.Application.DTOs.Users;

public class CreateUserDto
{
    public string FullName { get; set; } = null!;
    public string? City { get; set; }
    public string? Phone { get; set; }
    public string Email { get; set; } = null!;
}