using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.CartItems;

namespace TechNest.Application.Interfaces.Services;

public interface ICartItemService
{
    Task<PagedList<CartItemDto>> GetAllAsync(CartItemQueryParameters parameters, CancellationToken cancellationToken);
    Task<CartItemDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<CartItemDto> CreateAsync(CreateCartItemDto dto, CancellationToken cancellationToken);
    Task<CartItemDto> UpdateAsync(UpdateCartItemDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}