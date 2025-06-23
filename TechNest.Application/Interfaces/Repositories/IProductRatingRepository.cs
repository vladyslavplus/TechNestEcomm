using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Domain.Entities;

namespace TechNest.Application.Interfaces.Repositories;

public interface IProductRatingRepository : IGenericRepository<ProductRating>
{
    Task<PagedList<ProductRating>> GetAllPaginatedAsync(ProductRatingQueryParameters parameters, ISortHelper<ProductRating> sortHelper, CancellationToken cancellationToken = default);
}