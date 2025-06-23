using Microsoft.EntityFrameworkCore;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Domain.Entities;
using TechNest.Persistence.Data;
using TechNest.Persistence.Extensions;

namespace TechNest.Persistence.Repositories;

public class OrderItemRepository(ApplicationDbContext context) : GenericRepository<OrderItem>(context), IOrderItemRepository
{
    public async Task<PagedList<OrderItem>> GetAllPaginatedAsync(OrderItemQueryParameters parameters, ISortHelper<OrderItem> sortHelper,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();

        if (parameters.OrderId.HasValue)
            query = query.Where(oi => oi.OrderId == parameters.OrderId);

        if (parameters.ProductId.HasValue)
            query = query.Where(oi => oi.ProductId == parameters.ProductId);

        if (parameters.MinQuantity.HasValue)
            query = query.Where(oi => oi.Quantity >= parameters.MinQuantity.Value);

        if (parameters.MaxQuantity.HasValue)
            query = query.Where(oi => oi.Quantity <= parameters.MaxQuantity.Value);

        if (parameters.MinUnitPrice.HasValue)
            query = query.Where(oi => oi.UnitPrice >= parameters.MinUnitPrice.Value);

        if (parameters.MaxUnitPrice.HasValue)
            query = query.Where(oi => oi.UnitPrice <= parameters.MaxUnitPrice.Value);

        query = sortHelper.ApplySort(query, parameters.OrderBy);

        return await query
            .Include(oi => oi.Product)
            .Include(oi => oi.Order)
            .AsNoTracking()
            .ToPagedListAsync(parameters.PageNumber, parameters.PageSize, cancellationToken);
    }
}