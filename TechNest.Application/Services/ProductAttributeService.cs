using Mapster;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.ProductAttributes;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Application.Interfaces.Services;
using TechNest.Domain.Entities;

namespace TechNest.Application.Services;

public class ProductAttributeService(IUnitOfWork unitOfWork, ISortHelper<ProductAttribute> sortHelper) : IProductAttributeService
{
    public async Task<PagedList<ProductAttributeDto>> GetAllAsync(ProductAttributeQueryParameters parameters, CancellationToken cancellationToken)
    {
        var attributes = await unitOfWork.ProductAttributes.GetAllPaginatedAsync(parameters, sortHelper, cancellationToken);
        return attributes.Adapt<PagedList<ProductAttributeDto>>();
    }

    public async Task<ProductAttributeDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var attribute = await unitOfWork.ProductAttributes.GetByIdAsync(id, cancellationToken);
        return attribute?.Adapt<ProductAttributeDto>();
    }

    public async Task<ProductAttributeDto> CreateAsync(CreateProductAttributeDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Adapt<ProductAttribute>();
        await unitOfWork.ProductAttributes.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.Adapt<ProductAttributeDto>();
    }

    public async Task<ProductAttributeDto> UpdateAsync(UpdateProductAttributeDto dto, CancellationToken cancellationToken)
    {
        var attribute = await unitOfWork.ProductAttributes.GetByIdAsync(dto.Id, cancellationToken);
        if (attribute is null) throw new KeyNotFoundException("Product attribute not found");

        dto.Adapt(attribute);
        unitOfWork.ProductAttributes.Update(attribute);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return attribute.Adapt<ProductAttributeDto>();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var attribute = await unitOfWork.ProductAttributes.GetByIdAsync(id, cancellationToken);
        if (attribute is null) throw new KeyNotFoundException("Product attribute not found");

        unitOfWork.ProductAttributes.Delete(attribute);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}