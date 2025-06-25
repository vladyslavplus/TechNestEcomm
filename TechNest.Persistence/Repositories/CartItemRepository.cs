using Microsoft.EntityFrameworkCore;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Domain.Entities;
using TechNest.Persistence.Data;
using TechNest.Persistence.Extensions;

namespace TechNest.Persistence.Repositories;

public class CartItemRepository(ApplicationDbContext context) : GenericRepository<CartItem>(context), ICartItemRepository
{
    public async Task<PagedList<CartItem>> GetAllPaginatedAsync(CartItemQueryParameters parameters, ISortHelper<CartItem> sortHelper,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();

        if (parameters.UserId.HasValue)
            query = query.Where(ci => ci.UserId == parameters.UserId);

        if (parameters.ProductId.HasValue)
            query = query.Where(ci => ci.ProductId == parameters.ProductId);

        query = sortHelper.ApplySort(query, parameters.OrderBy);

        return await query
            .Include(ci => ci.Product) 
            .AsNoTracking()
            .ToPagedListAsync(parameters.PageNumber, parameters.PageSize, cancellationToken);
    }
    
    public async Task<CartItem?> GetByIdWithProductAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(ci => ci.Product)
            .AsNoTracking()
            .FirstOrDefaultAsync(ci => ci.Id == id, cancellationToken);
    }
}