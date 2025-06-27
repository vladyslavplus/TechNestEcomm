using TechNest.Domain.Entities.Enums;

namespace TechNest.Application.DTOs.Orders;

public class UpdateOrderDto
{
    public Guid Id { get; set; }
    public OrderStatus? Status { get; set; }
    public string? Department { get; set; }
    public string? City { get; set; }
    public string? Phone { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
}