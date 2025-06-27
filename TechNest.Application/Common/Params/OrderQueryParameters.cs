using TechNest.Domain.Entities.Enums;

namespace TechNest.Application.Common.Params;

public class OrderQueryParameters : QueryParameters
{
    public Guid? UserId { get; set; }
    public string? FullName { get; set; }
    public string? City { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Department { get; set; } 
    public string? Status { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }
}