namespace TechNest.Application.Common.Params;

public class UserQueryParameters : QueryParameters
{
    public string? FullName { get; set; }
    public string? City { get; set; }
    public string? Email { get; set; }
}