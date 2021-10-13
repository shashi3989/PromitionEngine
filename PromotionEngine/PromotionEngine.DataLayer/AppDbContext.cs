using Elasticsearch.Net;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Promotion>(b =>
            {
                b.Property(u => u.PromoProducts)
                    .HasConversion(
                        d => JsonConvert.SerializeObject(d),
                        s => JsonConvert.DeserializeObject<Dictionary<string, int>>(s)
                    )
                    .HasMaxLength(5000)
                    .IsRequired();
            });
        }

        public DbSet<Promotion> Promotions { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
