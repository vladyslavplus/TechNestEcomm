using TechNest.Application.Interfaces.Repositories;
using TechNest.Persistence.Data;

namespace TechNest.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IUserRepository Users { get; }
    public ICartItemRepository CartItems { get; }
    public ICategoryRepository Categories { get; }
    public IFavoriteProductRepository FavoriteProducts { get; }
    public IOrderRepository Orders { get; }
    public IOrderItemRepository OrderItems { get; }
    public IProductRepository Products { get; }
    public IProductAttributeRepository ProductAttributes { get; }
    public IProductCommentRepository ProductComments { get; }
    public IProductRatingRepository ProductRatings { get; }
    
    public UnitOfWork(ApplicationDbContext context, IUserRepository users, ICartItemRepository cartItems, ICategoryRepository categories, IFavoriteProductRepository favoriteProducts, IOrderRepository orders, IOrderItemRepository orderItems, IProductRepository products, IProductAttributeRepository productAttributes, IProductCommentRepository productComments, IProductRatingRepository productRatings)
    {
        _context = context;
        Users = users;
        CartItems = cartItems;
        Categories = categories;
        FavoriteProducts = favoriteProducts;
        Orders = orders;
        OrderItems = orderItems;
        Products = products;
        ProductAttributes = productAttributes;
        ProductComments = productComments;
        ProductRatings = productRatings;
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();
}