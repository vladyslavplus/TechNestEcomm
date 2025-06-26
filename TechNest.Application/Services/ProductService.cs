using Mapster;
using TechNest.Application.Common.Interfaces.Helpers;
using TechNest.Application.Common.Pagination;
using TechNest.Application.Common.Params;
using TechNest.Application.DTOs.Products;
using TechNest.Application.Interfaces.Repositories;
using TechNest.Application.Interfaces.Services;
using TechNest.Domain.Entities;

namespace TechNest.Application.Services;

public class ProductService(IUnitOfWork unitOfWork, ISortHelper<Product> sortHelper) : IProductService
{
    public async Task<PagedList<ProductDto>> GetAllAsync(ProductQueryParameters parameters, CancellationToken cancellationToken)
    {
        var products = await unitOfWork.Products.GetAllPaginatedAsync(parameters, sortHelper, cancellationToken);
        return products.Adapt<PagedList<ProductDto>>();
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id, cancellationToken);
        return product?.Adapt<ProductDto>();
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Adapt<Product>();
        await unitOfWork.Products.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.Adapt<ProductDto>();
    }

    public async Task<ProductDto> UpdateAsync(UpdateProductDto dto, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetByIdAsync(dto.Id, cancellationToken);
        if (product is null) throw new KeyNotFoundException("Product not found");

        dto.Adapt(product);
        unitOfWork.Products.Update(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Adapt<ProductDto>();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id, cancellationToken);
        if (product is null) throw new KeyNotFoundException("Product not found");

        unitOfWork.Products.Delete(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}