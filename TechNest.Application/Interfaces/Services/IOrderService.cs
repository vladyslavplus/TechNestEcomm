using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.Orders;

namespace TechNest.Application.Interfaces.Services;

public interface IOrderService
{
    Task<PagedList<OrderDto>> GetAllAsync(OrderQueryParameters parameters, CancellationToken cancellationToken);
    Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<OrderDto> CreateAsync(CreateOrderDto dto, CancellationToken cancellationToken);
    Task<OrderDto> UpdateAsync(UpdateOrderDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}