using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine.PromotionEngine.DataLayer
{
    public interface IPromotionEngineDataRepo
    {
        IEnumerable<Promotion> GetPromotionByProductName(CartItem cartItem);

        int GetProductValue(string productName);
    }
}
