using Microsoft.AspNetCore.Builder;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using PromotionEngine.Models;

namespace PromotionEngine.PromotionEngine.DataLayer
{
    public class PrepDb
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
                    new Promotion() {  },
                    new Promotion() {  },
                    new Promotion() {  }
                );

                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product() { Name = "A", Price = 50 },
                    new Product() { Name = "B", Price = 30 },
                    new Product() { Name = "C", Price = 20 },
                    new Product() { Name = "D", Price = 10 }
                );

                context.SaveChanges();
            }
        }
    }
}
