using Microsoft.EntityFrameworkCore;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Domain.Entities;
using TechNest.Persistence.Data;
using TechNest.Persistence.Extensions;

namespace TechNest.Persistence.Repositories;

public class ProductRatingRepository(ApplicationDbContext context) : GenericRepository<ProductRating>(context), IProductRatingRepository
{
    public async Task<PagedList<ProductRating>> GetAllPaginatedAsync(ProductRatingQueryParameters parameters, ISortHelper<ProductRating> sortHelper,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();

        if (parameters.ProductId.HasValue)
            query = query.Where(r => r.ProductId == parameters.ProductId.Value);

        if (parameters.UserId.HasValue)
            query = query.Where(r => r.UserId == parameters.UserId.Value);

        if (parameters.MinValue.HasValue)
            query = query.Where(r => r.Value >= parameters.MinValue.Value);

        if (parameters.MaxValue.HasValue)
            query = query.Where(r => r.Value <= parameters.MaxValue.Value);

        query = sortHelper.ApplySort(query, parameters.OrderBy);

        return await query
            .Include(r => r.Product)
            .Include(r => r.User)
            .AsNoTracking()
            .ToPagedListAsync(parameters.PageNumber, parameters.PageSize, cancellationToken);
    }
}