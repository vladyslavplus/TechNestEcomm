using Mapster;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.ProductComments;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Application.Interfaces.Services;
using TechNest.Domain.Entities;

namespace TechNest.Application.Services;

public class ProductCommentService(IUnitOfWork unitOfWork, ISortHelper<ProductComment> sortHelper) : IProductCommentService
{
    public async Task<PagedList<ProductCommentDto>> GetAllAsync(ProductCommentQueryParameters parameters, CancellationToken cancellationToken)
    {
        var comments = await unitOfWork.ProductComments.GetAllPaginatedAsync(parameters, sortHelper, cancellationToken);
        return comments.Adapt<PagedList<ProductCommentDto>>();
    }

    public async Task<ProductCommentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var comment = await unitOfWork.ProductComments.GetByIdAsync(id, cancellationToken);
        return comment?.Adapt<ProductCommentDto>();
    }

    public async Task<ProductCommentDto> CreateAsync(CreateProductCommentDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Adapt<ProductComment>();
        await unitOfWork.ProductComments.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.Adapt<ProductCommentDto>();
    }

    public async Task<ProductCommentDto> UpdateAsync(UpdateProductCommentDto dto, CancellationToken cancellationToken)
    {
        var comment = await unitOfWork.ProductComments.GetByIdAsync(dto.Id, cancellationToken);
        if (comment is null) throw new KeyNotFoundException("Product comment not found");

        dto.Adapt(comment);
        unitOfWork.ProductComments.Update(comment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return comment.Adapt<ProductCommentDto>();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var comment = await unitOfWork.ProductComments.GetByIdAsync(id, cancellationToken);
        if (comment is null) throw new KeyNotFoundException("Product comment not found");

        unitOfWork.ProductComments.Delete(comment);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}