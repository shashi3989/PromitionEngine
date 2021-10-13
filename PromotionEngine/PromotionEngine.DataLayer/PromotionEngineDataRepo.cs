using Microsoft.EntityFrameworkCore;
using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine.PromotionEngine.DataLayer
{
    public class PromotionEngineDataRepo: IPromotionEngineDataRepo
    {
        private readonly AppDbContext _context;
        public PromotionEngineDataRepo(AppDbContext context)
        {
            _context = context;
        }

        public int GetProductValue(string productName)
        {
            return _context.Products.FirstOrDefault(x=>x.Name==productName).Price;
        }

        public IEnumerable<Promotion> GetPromotionByProductName(CartItem cartItem)
        {
            return _context.Promotions
                           .Where(p => p.PromoProducts.ContainsKey(cartItem.Name) && p.PromoProducts[cartItem.Name]<=cartItem.Quantity)
                           .ToList();
        }
    }
}
