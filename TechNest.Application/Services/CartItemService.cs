using Mapster;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.CartItems;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Application.Interfaces.Services;
using TechNest.Domain.Entities;

namespace TechNest.Application.Services;

public class CartItemService(IUnitOfWork unitOfWork, ISortHelper<CartItem> sortHelper) : ICartItemService
{
    public async Task<PagedList<CartItemDto>> GetAllAsync(CartItemQueryParameters parameters, CancellationToken cancellationToken)
    {
        var items = await unitOfWork.CartItems.GetAllPaginatedAsync(parameters, sortHelper, cancellationToken);
        return items.Adapt<PagedList<CartItemDto>>();
    }

    public async Task<CartItemDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await unitOfWork.CartItems.GetByIdAsync(id, cancellationToken);
        return item?.Adapt<CartItemDto>();
    }

    public async Task<CartItemDto> CreateAsync(CreateCartItemDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Adapt<CartItem>();
        await unitOfWork.CartItems.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.Adapt<CartItemDto>();
    }

    public async Task<CartItemDto> UpdateAsync(UpdateCartItemDto dto, CancellationToken cancellationToken)
    {
        var item = await unitOfWork.CartItems.GetByIdAsync(dto.Id, cancellationToken);
        if (item is null) throw new Exception("Cart item not found");

        item.Quantity = dto.Quantity;
        unitOfWork.CartItems.Update(item);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return item.Adapt<CartItemDto>();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await unitOfWork.CartItems.GetByIdAsync(id, cancellationToken);
        if (item is null) throw new Exception("Cart item not found");

        unitOfWork.CartItems.Delete(item);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}