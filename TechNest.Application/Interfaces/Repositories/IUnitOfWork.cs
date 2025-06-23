namespace TechNest.Application.Interfaces.Repositories;

public interface IUnitOfWork : IAsyncDisposable
{
    IUserRepository Users { get; }
    ICartItemRepository CartItems { get; }
    ICategoryRepository Categories { get; }
    IFavoriteProductRepository FavoriteProducts { get; }
    IOrderRepository Orders { get; }
    IOrderItemRepository OrderItems { get; }
    IProductRepository Products { get; }
    IProductAttributeRepository ProductAttributes { get; }
    IProductCommentRepository ProductComments { get; }
    IProductRatingRepository ProductRatings { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}