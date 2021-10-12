using Microsoft.EntityFrameworkCore;
using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine.PromotionEngine.DataLayer
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Promotion> Promotions { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
