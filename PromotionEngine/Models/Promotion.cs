using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PromotionEngine.Models
{
    public class Promotion
    {
        public Promotion()
        {
            PromoProducts = new Dictionary<string, int>();
        }

        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }

        [Column("properties"), JsonExtensionData]
        public virtual Dictionary<string, int> PromoProducts { get; set; }

    }
}
