using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine.Models
{
    public class Product
    {
        [Key]
        public string Name { get; set; }

        public int Price { get; set; }
    }
}
