using Mapster;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.Orders;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Application.Interfaces.Services;
using TechNest.Domain.Entities;
using TechNest.Domain.Entities.Enums;

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
        var order = await unitOfWork.Orders.GetByIdWithItemsAsync(id, cancellationToken);
        return order?.Adapt<OrderDto>();
    }

    public async Task<OrderDto> CreateAsync(CreateOrderDto dto, CancellationToken cancellationToken)
    {
        var order = dto.Adapt<Order>();
        order.Items = new List<OrderItem>();

        foreach (var itemDto in dto.Items)
        {
            var product = await unitOfWork.Products.GetByIdAsync(itemDto.ProductId, cancellationToken);
            if (product == null) throw new KeyNotFoundException("Product not found");

            order.Items.Add(new OrderItem
            {
                ProductId = product.Id,
                Quantity = itemDto.Quantity,
                UnitPrice = product.Price
            });
        }

        await unitOfWork.Orders.AddAsync(order, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var created = await unitOfWork.Orders.GetByIdWithItemsAsync(order.Id, cancellationToken);
        return created!.Adapt<OrderDto>();
    }

    public async Task<OrderDto> UpdateAsync(UpdateOrderDto dto, CancellationToken cancellationToken)
    {
        var order = await unitOfWork.Orders.GetByIdWithItemsAsync(dto.Id, cancellationToken);
        if (order is null) throw new KeyNotFoundException("Order not found");

        if (dto.Status.HasValue)
            order.Status = dto.Status.Value;

        if (!string.IsNullOrWhiteSpace(dto.Department))
            order.Department = dto.Department;

        if (!string.IsNullOrWhiteSpace(dto.City))
            order.City = dto.City;

        if (!string.IsNullOrWhiteSpace(dto.Phone))
            order.Phone = dto.Phone;

        if (!string.IsNullOrWhiteSpace(dto.Email))
            order.Email = dto.Email;

        if (!string.IsNullOrWhiteSpace(dto.FullName))
            order.FullName = dto.FullName;

        unitOfWork.Orders.Update(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var updated = await unitOfWork.Orders.GetByIdWithItemsAsync(order.Id, cancellationToken);
        return updated!.Adapt<OrderDto>();
    }


    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await unitOfWork.Orders.GetByIdAsync(id, cancellationToken);
        if (order is null) throw new KeyNotFoundException("Order not found");

        unitOfWork.Orders.Delete(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}