using Mapster;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.Orders;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Application.Interfaces.Services;
using TechNest.Domain.Entities;

namespace TechNest.Application.Services;

public class OrderService(IUnitOfWork unitOfWork, ISortHelper<Order> sortHelper) : IOrderService
{
    public async Task<PagedList<OrderDto>> GetAllAsync(OrderQueryParameters parameters, CancellationToken cancellationToken)
    {
        var orders = await unitOfWork.Orders.GetAllPaginatedAsync(parameters, sortHelper, cancellationToken);
        return orders.Adapt<PagedList<OrderDto>>();
    }

    public async Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await unitOfWork.Orders.GetByIdAsync(id, cancellationToken);
        return order?.Adapt<OrderDto>();
    }

    public async Task<OrderDto> CreateAsync(CreateOrderDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Adapt<Order>();
        await unitOfWork.Orders.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.Adapt<OrderDto>();
    }

    public async Task<OrderDto> UpdateAsync(UpdateOrderDto dto, CancellationToken cancellationToken)
    {
        var order = await unitOfWork.Orders.GetByIdAsync(dto.Id, cancellationToken);
        if (order is null) throw new Exception("Order not found");

        order.Status = dto.Status;
        unitOfWork.Orders.Update(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Adapt<OrderDto>();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await unitOfWork.Orders.GetByIdAsync(id, cancellationToken);
        if (order is null) throw new Exception("Order not found");

        unitOfWork.Orders.Delete(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}