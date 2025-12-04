using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bloomfiy.Models
{
    [Table("ProductColors")]
    public class ProductColor
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("color_id")]
        public int ColorId { get; set; }

        // NEW: Image URL for THIS specific product-color combination
        [StringLength(500)]
        [Column("image_url")]
        public string ImageUrl { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("ColorId")]
        public virtual Color Color { get; set; }

        // Helper property to get full price
        [NotMapped]
        public string DefaultImageUrl
        {
            get
            {
                if (ProductColors != null)
                {
                    foreach (var pc in ProductColors)
                    {
                        if (pc != null && !string.IsNullOrEmpty(pc.ImageUrl))
                            return pc.ImageUrl;
                    }
                }
                return "/Images/default-flower.jpg";
            }
        }
    }
}