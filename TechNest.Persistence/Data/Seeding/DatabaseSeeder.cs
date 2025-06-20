using Bogus;
using Microsoft.EntityFrameworkCore;
using TechNest.Domain.Entities;
using TechNest.Persistence.Identity;

namespace TechNest.Persistence.Data.Seeding;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Categories.AnyAsync()) return;

        var faker = new Faker();
        var rnd = new Random();

        // 1. Categories
        var categories = new List<Category>
        {
            new Category { Id = Guid.NewGuid(), Name = "Smartphones" },
            new Category { Id = Guid.NewGuid(), Name = "Laptops" },
            new Category { Id = Guid.NewGuid(), Name = "Audio Equipment" },
            new Category { Id = Guid.NewGuid(), Name = "Home Appliances" },
        };
        await context.Categories.AddRangeAsync(categories);

        // 2. Products
        var productFaker = new Faker<Product>()
            .RuleFor(p => p.Id, _ => Guid.NewGuid())
            .RuleFor(p => p.Name, (f, _) => $"{f.Commerce.ProductName()} {f.Commerce.ProductAdjective()}")
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(50, 2000)))
            .RuleFor(p => p.Brand, f => f.Company.CompanyName())
            .RuleFor(p => p.ImageUrl, f => f.Image.PicsumUrl(200, 200))
            .RuleFor(p => p.Stock, f => f.Random.Int(0, 100))
            .RuleFor(p => p.CategoryId, f => f.PickRandom(categories).Id);

        var products = productFaker.Generate(50);
        await context.Products.AddRangeAsync(products);

        // 3. Product Attributes
        var attributeKeys = new[] { "Color", "Weight", "ScreenSize", "Battery", "Processor", "RAM" };
        var productAttributes = new List<ProductAttribute>();

        foreach (var product in products)
        {
            var attrCount = rnd.Next(1, 4);
            for (var i = 0; i < attrCount; i++)
            {
                productAttributes.Add(new ProductAttribute
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Key = attributeKeys[i],
                    Value = faker.Random.Word()
                });
            }
        }
        await context.ProductAttributes.AddRangeAsync(productAttributes);

        // 4. Users (ApplicationUser)
        var userFaker = new Faker<ApplicationUser>()
            .RuleFor(u => u.Id, _ => Guid.NewGuid())
            .RuleFor(u => u.FullName, f => f.Name.FullName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.UserName, (_, u) => u.Email)
            .RuleFor(u => u.City, f => f.Address.City())
            .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
            .RuleFor(u => u.NormalizedEmail, (_, u) => u.Email?.ToUpper())
            .RuleFor(u => u.NormalizedUserName, (_, u) => u.UserName?.ToUpper())
            .RuleFor(u => u.EmailConfirmed, _ => true)
            .RuleFor(u => u.SecurityStamp, _ => Guid.NewGuid().ToString());

        var users = userFaker.Generate(10);
        await context.Users.AddRangeAsync(users);

        // 5. Orders
        var orderFaker = new Faker<Order>()
            .RuleFor(o => o.Id, _ => Guid.NewGuid())
            .RuleFor(o => o.UserId, f => f.PickRandom(users).Id)
            .RuleFor(o => o.FullName, (_, o) => users.First(u => u.Id == o.UserId).FullName)
            .RuleFor(o => o.Email, (_, o) => users.First(u => u.Id == o.UserId).Email)
            .RuleFor(o => o.Phone, (f, o) => users.First(u => u.Id == o.UserId).Phone ?? f.Phone.PhoneNumber())
            .RuleFor(o => o.City, f => f.Address.City())
            .RuleFor(o => o.Department, f => $"Post Office Branch #{f.Random.Int(1, 100)}")
            .RuleFor(o => o.CreatedAt, f => f.Date.Past().ToUniversalTime());

        var orders = orderFaker.Generate(20);
        await context.Orders.AddRangeAsync(orders);

        // 6. Order Items
        var orderItems = new List<OrderItem>();
        foreach (var order in orders)
        {
            var itemsCount = rnd.Next(1, 5);
            var orderProducts = faker.PickRandom(products, itemsCount).ToList();

            orderItems.AddRange(orderProducts.Select(prod => new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = prod.Id,
                Quantity = rnd.Next(1, 3),
                UnitPrice = prod.Price
            }));
        }
        await context.OrderItems.AddRangeAsync(orderItems);

        // 7. Cart Items
        var cartItems = new List<CartItem>();
        foreach (var user in users)
        {
            var cartCount = rnd.Next(0, 5);
            var cartProducts = faker.PickRandom(products, cartCount).ToList();

            cartItems.AddRange(cartProducts.Select(prod => new CartItem
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                ProductId = prod.Id,
                Quantity = rnd.Next(1, 5),
                AddedAt = DateTime.UtcNow
            }));
        }
        await context.CartItems.AddRangeAsync(cartItems);

        // 8. Favorite Products
        var favoriteProducts = new List<FavoriteProduct>();
        foreach (var user in users)
        {
            var favCount = rnd.Next(0, 5);
            var favProducts = faker.PickRandom(products, favCount).ToList();

            favoriteProducts.AddRange(favProducts.Select(prod => new FavoriteProduct
            {
                UserId = user.Id,
                ProductId = prod.Id
            }));
        }
        await context.FavoriteProducts.AddRangeAsync(favoriteProducts);

        // 9. Product Ratings
        var ratings = new List<ProductRating>();
        foreach (var user in users)
        {
            var rateCount = rnd.Next(0, 5);
            var rateProducts = faker.PickRandom(products, rateCount).ToList();

            ratings.AddRange(rateProducts.Select(prod => new ProductRating
            {
                Id = Guid.NewGuid(),
                ProductId = prod.Id,
                UserId = user.Id,
                Value = faker.Random.Int(1, 5),
                RatedAt = DateTime.UtcNow.AddDays(-faker.Random.Int(0, 365))
            }));
        }
        await context.ProductRatings.AddRangeAsync(ratings);

        // 10. Product Comments
        var comments = new List<ProductComment>();
        foreach (var user in users)
        {
            var commentCount = rnd.Next(0, 5);
            var commentProducts = faker.PickRandom(products, commentCount).ToList();

            comments.AddRange(commentProducts.Select(prod => new ProductComment
            {
                Id = Guid.NewGuid(),
                ProductId = prod.Id,
                UserId = user.Id,
                Text = faker.Lorem.Sentence(),
                CreatedAt = DateTime.UtcNow.AddDays(-faker.Random.Int(0, 365)) 
            }));
        }
        await context.ProductComments.AddRangeAsync(comments);

        await context.SaveChangesAsync();
    }
}
