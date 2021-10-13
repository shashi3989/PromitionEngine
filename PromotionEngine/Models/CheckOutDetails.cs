using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine.Models
{
    public class CheckOutDetails
    {
        public List<CartBilling> Items { get; set; }
        public int TotalAmount { get; set; }
    }
}
