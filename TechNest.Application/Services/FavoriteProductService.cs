using Mapster;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.FavoriteProducts;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Application.Interfaces.Services;
using TechNest.Domain.Entities;

namespace TechNest.Application.Services;

public class FavoriteProductService(IUnitOfWork unitOfWork, ISortHelper<FavoriteProduct> sortHelper) : IFavoriteProductService
{
    public async Task<PagedList<FavoriteProductDto>> GetAllAsync(FavoriteProductQueryParameters parameters, CancellationToken cancellationToken)
    {
        var favorites = await unitOfWork.FavoriteProducts.GetAllPaginatedAsync(parameters, sortHelper, cancellationToken);
        return favorites.Adapt<PagedList<FavoriteProductDto>>();
    }
    
    public async Task<FavoriteProductDto?> GetByUserIdAndProductIdAsync(Guid userId, Guid productId, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.FavoriteProducts.GetByUserIdAndProductIdAsync(userId, productId, cancellationToken);
        return entity?.Adapt<FavoriteProductDto>();
    }

    public async Task<FavoriteProductDto> CreateAsync(CreateFavoriteProductDto dto, CancellationToken cancellationToken)
    {
        var exists = await unitOfWork.FavoriteProducts.GetByUserIdAndProductIdAsync(dto.UserId, dto.ProductId, cancellationToken);

        if (exists != null)
            throw new InvalidOperationException("This product is already in favorites.");

        var entity = dto.Adapt<FavoriteProduct>();
        await unitOfWork.FavoriteProducts.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var created = await unitOfWork.FavoriteProducts.GetByUserIdAndProductIdAsync(dto.UserId, dto.ProductId, cancellationToken);
        return created!.Adapt<FavoriteProductDto>();
    }

    public async Task DeleteAsync(Guid userId, Guid productId, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.FavoriteProducts.GetByUserIdAndProductIdAsync(userId, productId, cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException("Favorite product not found");

        unitOfWork.FavoriteProducts.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
