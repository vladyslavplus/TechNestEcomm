using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechNest.Domain.Entities;
using TechNest.Persistence.Identity;

namespace TechNest.Persistence.Data.Seeding;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        if (await context.Categories.AnyAsync()) return;

        var faker = new Faker();
        var rnd = new Random();

        // 1. Categories
        var categories = new List<Category>
        {
            new() { Id = Guid.NewGuid(), Name = "Smartphones" },
            new() { Id = Guid.NewGuid(), Name = "Laptops" },
            new() { Id = Guid.NewGuid(), Name = "Audio Equipment" },
            new() { Id = Guid.NewGuid(), Name = "Home Appliances" },
            new() { Id = Guid.NewGuid(), Name = "Gaming" },
            new() { Id = Guid.NewGuid(), Name = "Accessories" }
        };
        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();

        // 2. Products
        var productFaker = new Faker<Product>()
            .RuleFor(p => p.Id, _ => Guid.NewGuid())
            .RuleFor(p => p.Name, f => $"{f.Commerce.ProductName()} {f.Commerce.ProductAdjective()}")
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(50, 2000)))
            .RuleFor(p => p.Brand, f => f.Company.CompanyName())
            .RuleFor(p => p.ImageUrl, f => f.Image.PicsumUrl(400, 400))
            .RuleFor(p => p.Stock, f => f.Random.Int(0, 100))
            .RuleFor(p => p.CategoryId, f => f.PickRandom(categories).Id);

        var products = productFaker.Generate(60);
        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();

        // 3. Product Attributes
        var attributeKeys = new[] { "Color", "Weight", "Screen Size", "Battery Life", "Processor", "RAM", "Storage", "Display Type", "Operating System", "Warranty" };
        var attributeValues = new Dictionary<string, string[]>
        {
            ["Color"] = ["Black", "White", "Silver", "Gold", "Blue", "Red", "Green"],
            ["Weight"] = ["150g", "200g", "1.2kg", "2.5kg", "500g", "1kg"],
            ["Screen Size"] = ["5.5\"", "6.1\"", "13.3\"", "15.6\"", "27\"", "32\""],
            ["Battery Life"] = ["8 hours", "12 hours", "24 hours", "3 days", "1 week"],
            ["Processor"] = ["Intel i5", "Intel i7", "AMD Ryzen 5", "AMD Ryzen 7", "Apple M1", "Snapdragon 888"],
            ["RAM"] = ["4GB", "8GB", "16GB", "32GB", "64GB"],
            ["Storage"] = ["128GB", "256GB", "512GB", "1TB", "2TB"],
            ["Display Type"] = ["OLED", "LCD", "LED", "AMOLED", "IPS"],
            ["Operating System"] = ["Windows 11", "macOS", "Android", "iOS", "Linux"],
            ["Warranty"] = ["1 year", "2 years", "3 years", "5 years"]
        };

        var productAttributes = new List<ProductAttribute>();
        foreach (var product in products)
        {
            var attrCount = rnd.Next(2, 5);
            var selectedKeys = faker.PickRandom(attributeKeys, attrCount).ToArray();

            foreach (var key in selectedKeys)
            {
                var value = attributeValues.TryGetValue(key, out var attributeValue) 
                    ? faker.PickRandom(attributeValue)
                    : faker.Random.Word();

                productAttributes.Add(new ProductAttribute
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Key = key,
                    Value = value
                });
            }
        }
        await context.ProductAttributes.AddRangeAsync(productAttributes);
        await context.SaveChangesAsync();

        // 4. Users 
        var createdUsers = new List<ApplicationUser>();
        var userFaker = new Faker<ApplicationUser>()
            .RuleFor(u => u.FullName, f => f.Name.FullName())
            .RuleFor(u => u.City, f => f.Address.City())
            .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
            .RuleFor(u => u.EmailConfirmed, _ => true);

        for (var i = 0; i < 15; i++)
        {
            var email = faker.Internet.Email();
            var user = userFaker.Generate();
            user.UserName = email;
            user.Email = email;

            var result = await userManager.CreateAsync(user, "Test123!");
            if (!result.Succeeded) continue;
            var savedUser = await userManager.FindByEmailAsync(email);
            if (savedUser != null)
            {
                createdUsers.Add(savedUser);
            }
        }

        if (createdUsers.Count == 0)
            throw new InvalidOperationException("Failed to create any users during seeding");

        // 5. Orders
        var orders = new List<Order>();
        foreach (var user in createdUsers)
        {
            var orderCount = rnd.Next(0, 4); 
            for (var i = 0; i < orderCount; i++)
            {
                orders.Add(new Order
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    FullName = user.FullName,
                    Email = user.Email!,
                    Phone = user.Phone ?? faker.Phone.PhoneNumber(),
                    City = user.City ?? faker.Address.City(),
                    Department = $"Post Office Branch #{faker.Random.Int(1, 100)}",
                    Status = faker.PickRandom<Domain.Entities.Enums.OrderStatus>(),
                    CreatedAt = faker.Date.Past().ToUniversalTime()
                });
            }
        }
        
        if (orders.Count != 0)
        {
            await context.Orders.AddRangeAsync(orders);
            await context.SaveChangesAsync();
        }

        // 6. Order Items
        var orderItems = new List<OrderItem>();
        foreach (var order in orders)
        {
            var itemCount = rnd.Next(1, 6); 
            var orderProducts = faker.PickRandom(products, itemCount).ToList();
            
            foreach (var product in orderProducts)
            {
                orderItems.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = rnd.Next(1, 4),
                    UnitPrice = product.Price
                });
            }
        }
        
        if (orderItems.Count != 0)
        {
            await context.OrderItems.AddRangeAsync(orderItems);
            await context.SaveChangesAsync();
        }

        // 7. Cart Items
        var cartItems = new List<CartItem>();
        foreach (var user in createdUsers)
        {
            var cartProductCount = rnd.Next(0, 6);
            if (cartProductCount <= 0) continue;
            var cartProducts = faker.PickRandom(products, cartProductCount).ToList();
            foreach (var product in cartProducts)
            {
                cartItems.Add(new CartItem
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    ProductId = product.Id,
                    Quantity = rnd.Next(1, 4),
                    AddedAt = faker.Date.Recent(30).ToUniversalTime()
                });
            }
        }
        
        if (cartItems.Count != 0)
        {
            await context.CartItems.AddRangeAsync(cartItems);
            await context.SaveChangesAsync();
        }

        // 8. Favorite Products
        var favorites = new List<FavoriteProduct>();
        foreach (var user in createdUsers)
        {
            var favoriteCount = rnd.Next(0, 8);
            if (favoriteCount <= 0) continue;
            var favoriteProducts = faker.PickRandom(products, favoriteCount).ToList();
            foreach (var product in favoriteProducts)
            {
                favorites.Add(new FavoriteProduct
                {
                    UserId = user.Id,
                    ProductId = product.Id
                });
            }
        }
        
        if (favorites.Count != 0)
        {
            await context.FavoriteProducts.AddRangeAsync(favorites);
            await context.SaveChangesAsync();
        }

        // 9. Product Ratings
        var ratings = new List<ProductRating>();
        foreach (var user in createdUsers)
        {
            var ratingCount = rnd.Next(0, 10);
            if (ratingCount <= 0) continue;
            var ratedProducts = faker.PickRandom(products, ratingCount).ToList();
            foreach (var product in ratedProducts)
            {
                ratings.Add(new ProductRating
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    UserId = user.Id,
                    Value = faker.Random.Int(1, 5),
                    RatedAt = faker.Date.Past().ToUniversalTime()
                });
            }
        }
        
        if (ratings.Count != 0)
        {
            await context.ProductRatings.AddRangeAsync(ratings);
            await context.SaveChangesAsync();
        }

        // 10. Product Comments
        var comments = new List<ProductComment>();
        var commentTexts = new[]
        {
            "Excellent product, highly recommend!",
            "Good value for money.",
            "Fast delivery and great packaging.",
            "Product matches the description perfectly.",
            "Outstanding quality and performance.",
            "Had some issues with setup, but works great now.",
            "Customer service was very helpful.",
            "Delivered on time, works as expected.",
            "Great design and functionality.",
            "Would buy again!"
        };

        foreach (var user in createdUsers)
        {
            var commentCount = rnd.Next(0, 7); 
            if (commentCount <= 0) continue;
            var commentedProducts = faker.PickRandom(products, commentCount).ToList();
            foreach (var product in commentedProducts)
            {
                comments.Add(new ProductComment
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    UserId = user.Id,
                    Text = faker.PickRandom(commentTexts),
                    CreatedAt = faker.Date.Past().ToUniversalTime()
                });
            }
        }
        
        if (comments.Count != 0)
        {
            await context.ProductComments.AddRangeAsync(comments);
            await context.SaveChangesAsync();
        }
    }
}