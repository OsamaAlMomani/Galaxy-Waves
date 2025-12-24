using ProductService.Models;

namespace ProductService.Data;

public static class DbInitializer
{
    public static void Seed(ProductDbContext context)
    {
        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product { Name = "Sample Product 1", Price = 9.99m },
                new Product { Name = "Sample Product 2", Price = 19.99m }
            );
            context.SaveChanges();
        }
    }
}