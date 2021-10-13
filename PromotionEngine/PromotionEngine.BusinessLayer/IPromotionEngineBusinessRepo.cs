using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine.PromotionEngine.BusinessLayer
{
    public interface IPromotionEngineBusinessRepo
    {
        int CalculateTotalAmount(CartItem[] items);
    }
}
