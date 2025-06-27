using TechNest.Application.DTOs.OrderItems;

namespace TechNest.Application.DTOs.Orders;

public class CreateOrderDto
{
    public Guid UserId { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Department { get; set; } = null!;
    public ICollection<CreateOrderOrderItemDto> Items { get; set; } = new List<CreateOrderOrderItemDto>();
}