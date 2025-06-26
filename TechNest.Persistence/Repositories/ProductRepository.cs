using Microsoft.EntityFrameworkCore;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Domain.Entities;
using TechNest.Persistence.Data;
using TechNest.Persistence.Extensions;

namespace TechNest.Persistence.Repositories;

public class ProductRepository(ApplicationDbContext context) : GenericRepository<Product>(context), IProductRepository
{
    public async Task<PagedList<Product>> GetAllPaginatedAsync(ProductQueryParameters parameters, ISortHelper<Product> sortHelper,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.Name))
            query = query.Where(p => p.Name.ToLower().Contains(parameters.Name.ToLower()));

        if (!string.IsNullOrWhiteSpace(parameters.Brand))
            query = query.Where(p => p.Brand.ToLower().Contains(parameters.Brand.ToLower()));

        if (!string.IsNullOrWhiteSpace(parameters.Description))
            query = query.Where(p => p.Description.ToLower().Contains(parameters.Description.ToLower()));

        if (parameters.CategoryId.HasValue)
            query = query.Where(p => p.CategoryId == parameters.CategoryId.Value);

        if (parameters.MinPrice.HasValue)
            query = query.Where(p => p.Price >= parameters.MinPrice.Value);

        if (parameters.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= parameters.MaxPrice.Value);

        if (parameters.MinStock.HasValue)
            query = query.Where(p => p.Stock >= parameters.MinStock.Value);

        if (parameters.MaxStock.HasValue)
            query = query.Where(p => p.Stock <= parameters.MaxStock.Value);

        query = sortHelper.ApplySort(query, parameters.OrderBy);

        return await query
            .Include(p => p.Category)
            .Include(p => p.Attributes)
            .Include(p => p.Ratings)
            .Include(p => p.Comments)
            .Include(p => p.Favorites)
            .AsNoTracking()
            .ToPagedListAsync(parameters.PageNumber, parameters.PageSize, cancellationToken);
    }
}