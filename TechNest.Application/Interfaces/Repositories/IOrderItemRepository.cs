using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Domain.Entities;

namespace TechNest.Application.Interfaces.Repositories;

public interface IOrderItemRepository : IGenericRepository<OrderItem>
{
    Task<PagedList<OrderItem>> GetAllPaginatedAsync(OrderItemQueryParameters parameters, ISortHelper<OrderItem> sortHelper, CancellationToken cancellationToken = default);
}