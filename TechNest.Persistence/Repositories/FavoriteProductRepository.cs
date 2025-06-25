using Microsoft.EntityFrameworkCore;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Domain.Entities;
using TechNest.Persistence.Data;
using TechNest.Persistence.Extensions;

namespace TechNest.Persistence.Repositories;

public class FavoriteProductRepository(ApplicationDbContext context) : GenericRepository<FavoriteProduct>(context), IFavoriteProductRepository
{
    public async Task<PagedList<FavoriteProduct>> GetAllPaginatedAsync(FavoriteProductQueryParameters parameters, ISortHelper<FavoriteProduct> sortHelper,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();

        if (parameters.UserId.HasValue)
            query = query.Where(f => f.UserId == parameters.UserId);

        if (parameters.ProductId.HasValue)
            query = query.Where(f => f.ProductId == parameters.ProductId);

        query = sortHelper.ApplySort(query, parameters.OrderBy);

        return await query
            .Include(f => f.Product)
            .ThenInclude(p => p.Category)
            .AsNoTracking()
            .ToPagedListAsync(parameters.PageNumber, parameters.PageSize, cancellationToken);
    }
}