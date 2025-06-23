using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Domain.Entities;

namespace TechNest.Application.Interfaces.Repositories;

public interface ICartItemRepository : IGenericRepository<CartItem>
{
    Task<PagedList<CartItem>> GetAllPaginatedAsync(CartItemQueryParameters parameters, ISortHelper<CartItem> sortHelper, CancellationToken cancellationToken = default);
}