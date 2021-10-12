using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine.Models
{
    public class Promotion
    {
      
        public Promotion()
        {
            PromoProducts = new Dictionary<string, int>();
            PromoProducts.Add("",1);
        }
        public int Id { get; set; }

        public int Amount { get; set; }

        public IDictionary<string, int> PromoProducts { get; set; }




    }
}
