using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.FavoriteProducts;

namespace TechNest.Application.Interfaces.Services;

public interface IFavoriteProductService
{
    Task<PagedList<FavoriteProductDto>> GetAllAsync(FavoriteProductQueryParameters parameters, CancellationToken cancellationToken);
    Task<FavoriteProductDto?> GetByUserIdAndProductIdAsync(Guid userId, Guid productId, CancellationToken cancellationToken);
    Task<FavoriteProductDto> CreateAsync(CreateFavoriteProductDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid userId, Guid productId, CancellationToken cancellationToken);
}