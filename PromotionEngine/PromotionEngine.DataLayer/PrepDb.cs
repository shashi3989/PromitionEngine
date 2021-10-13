using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using PromotionEngine.Models;
using System.Collections.Generic;

namespace PromotionEngine.PromotionEngine.DataLayer
{
    public static class PrepDb
    {
        public static void PrepPoulation(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }

        private static void SeedData(AppDbContext context)
        {

            if (!context.Promotions.Any())
            {
                context.Promotions.AddRange(
                     new Promotion() { Id = 1, Amount = 130, PromoProducts = new Dictionary<string, int>() { { "A", 3 } } },
                     new Promotion() { Id = 2, Amount = 45, PromoProducts = new Dictionary<string, int>() { { "B", 2 } } },
                     new Promotion() { Id = 3, Amount = 30, PromoProducts = new Dictionary<string, int>() { { "C", 1 }, { "D", 1 } } }
                );
                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product() { Name = "A", Price = 50 },
                    new Product() { Name = "B", Price = 30 },
                    new Product() { Name = "C", Price = 20 },
                    new Product() { Name = "D", Price = 15 }
                );

                context.SaveChanges();
            }
        }
    }
}
