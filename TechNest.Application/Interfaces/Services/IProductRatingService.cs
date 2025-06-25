using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.ProductRatings;

namespace TechNest.Application.Interfaces.Services;

public interface IProductRatingService
{
    Task<PagedList<ProductRatingDto>> GetAllAsync(ProductRatingQueryParameters parameters, CancellationToken cancellationToken);
    Task<ProductRatingDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ProductRatingDto> CreateAsync(CreateProductRatingDto dto, CancellationToken cancellationToken);
    Task<ProductRatingDto> UpdateAsync(UpdateProductRatingDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}