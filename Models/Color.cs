using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bloomfiy.Models
{
    [Table("Colors")]
    public class Color
    {
        [Key]
        [Column("color_id")]
        public int ColorId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("color_name")]
        public string ColorName { get; set; }

        [StringLength(10)]
        [Column("color_code")]
        public string ColorCode { get; set; }

        [Column("price_adjustment")]
        public decimal PriceAdjustment { get; set; } = 0;

        [Column("is_available")]
        public bool IsAvailable { get; set; } = true;

        // ✅ CORRECT: Navigation property for Products (many-to-many via ProductColors)
        public virtual ICollection<ProductColor> ProductColors { get; set; }
    }
}