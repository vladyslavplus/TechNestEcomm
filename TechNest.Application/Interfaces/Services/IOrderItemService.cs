using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.OrderItems;

namespace TechNest.Application.Interfaces.Services;

public interface IOrderItemService
{
    Task<PagedList<OrderItemDto>> GetAllAsync(OrderItemQueryParameters parameters, CancellationToken cancellationToken);
    Task<OrderItemDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<OrderItemDto> CreateAsync(CreateOrderItemDto dto, CancellationToken cancellationToken);
    Task<OrderItemDto> UpdateAsync(UpdateOrderItemDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}