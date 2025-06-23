using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Domain.Entities;

namespace TechNest.Application.Interfaces.Repositories;

public interface IFavoriteProductRepository : IGenericRepository<FavoriteProduct>
{
    Task<PagedList<FavoriteProduct>> GetAllPaginatedAsync(FavoriteProductQueryParameters parameters, ISortHelper<FavoriteProduct> sortHelper, CancellationToken cancellationToken = default);
}