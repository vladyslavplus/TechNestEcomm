using Microsoft.EntityFrameworkCore;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Domain.Entities;
using TechNest.Persistence.Data;
using TechNest.Persistence.Extensions;

namespace TechNest.Persistence.Repositories;

public class ProductAttributeRepository(ApplicationDbContext context) : GenericRepository<ProductAttribute>(context), IProductAttributeRepository
{
    public async Task<PagedList<ProductAttribute>> GetAllPaginatedAsync(ProductAttributeQueryParameters parameters, ISortHelper<ProductAttribute> sortHelper,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();

        if (parameters.ProductId.HasValue)
            query = query.Where(pa => pa.ProductId == parameters.ProductId.Value);

        if (!string.IsNullOrWhiteSpace(parameters.Key))
            query = query.Where(pa => pa.Key.Contains(parameters.Key, StringComparison.CurrentCultureIgnoreCase));

        if (!string.IsNullOrWhiteSpace(parameters.Value))
            query = query.Where(pa => pa.Value.Contains(parameters.Value, StringComparison.CurrentCultureIgnoreCase));

        query = sortHelper.ApplySort(query, parameters.OrderBy);

        return await query
            .Include(pa => pa.Product)
            .AsNoTracking()
            .ToPagedListAsync(parameters.PageNumber, parameters.PageSize, cancellationToken);
    }
}