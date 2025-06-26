using Mapster;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.ProductRatings;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Application.Interfaces.Services;
using TechNest.Domain.Entities;

namespace TechNest.Application.Services;

public class ProductRatingService(IUnitOfWork unitOfWork, ISortHelper<ProductRating> sortHelper) : IProductRatingService
{
    public async Task<PagedList<ProductRatingDto>> GetAllAsync(ProductRatingQueryParameters parameters, CancellationToken cancellationToken)
    {
        var ratings = await unitOfWork.ProductRatings.GetAllPaginatedAsync(parameters, sortHelper, cancellationToken);
        return ratings.Adapt<PagedList<ProductRatingDto>>();
    }

    public async Task<ProductRatingDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var rating = await unitOfWork.ProductRatings.GetByIdAsync(id, cancellationToken);
        return rating?.Adapt<ProductRatingDto>();
    }

    public async Task<ProductRatingDto> CreateAsync(CreateProductRatingDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Adapt<ProductRating>();
        await unitOfWork.ProductRatings.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.Adapt<ProductRatingDto>();
    }

    public async Task<ProductRatingDto> UpdateAsync(UpdateProductRatingDto dto, CancellationToken cancellationToken)
    {
        var rating = await unitOfWork.ProductRatings.GetByIdAsync(dto.Id, cancellationToken);
        if (rating is null) throw new KeyNotFoundException("Product rating not found");

        dto.Adapt(rating);
        unitOfWork.ProductRatings.Update(rating);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return rating.Adapt<ProductRatingDto>();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var rating = await unitOfWork.ProductRatings.GetByIdAsync(id, cancellationToken);
        if (rating is null) throw new KeyNotFoundException("Product rating not found");

        unitOfWork.ProductRatings.Delete(rating);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}