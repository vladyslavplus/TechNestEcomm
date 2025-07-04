﻿using Mapster;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.OrderItems;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Application.Interfaces.Services;
using TechNest.Domain.Entities;

namespace TechNest.Application.Services;

public class OrderItemService(IUnitOfWork unitOfWork, ISortHelper<OrderItem> sortHelper) : IOrderItemService
{
    public async Task<PagedList<OrderItemDto>> GetAllAsync(OrderItemQueryParameters parameters, CancellationToken cancellationToken)
    {
        var items = await unitOfWork.OrderItems.GetAllPaginatedAsync(parameters, sortHelper, cancellationToken);
        return items.Adapt<PagedList<OrderItemDto>>();
    }

    public async Task<OrderItemDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await unitOfWork.OrderItems.GetByIdWithDetailsAsync(id, cancellationToken);
        return item?.Adapt<OrderItemDto>();
    }

    public async Task<OrderItemDto> CreateAsync(CreateOrderItemDto dto, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetByIdAsync(dto.ProductId, cancellationToken);
        if (product is null)
            throw new KeyNotFoundException("Product not found");

        var entity = new OrderItem
        {
            OrderId = dto.OrderId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            UnitPrice = product.Price 
        };

        await unitOfWork.OrderItems.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var created = await unitOfWork.OrderItems.GetByIdWithDetailsAsync(entity.Id, cancellationToken);
        return created!.Adapt<OrderItemDto>();
    }

    public async Task<OrderItemDto> UpdateAsync(UpdateOrderItemDto dto, CancellationToken cancellationToken)
    {
        var item = await unitOfWork.OrderItems.GetByIdWithDetailsAsync(dto.Id, cancellationToken);
        if (item is null) throw new KeyNotFoundException("Order item not found");

        item.Quantity = dto.Quantity;

        item.UnitPrice = item.Product.Price;

        unitOfWork.OrderItems.Update(item);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var updated = await unitOfWork.OrderItems.GetByIdWithDetailsAsync(dto.Id, cancellationToken);
        return updated!.Adapt<OrderItemDto>();
    }


    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await unitOfWork.OrderItems.GetByIdAsync(id, cancellationToken);
        if (item is null) throw new KeyNotFoundException("Order item not found");

        unitOfWork.OrderItems.Delete(item);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}