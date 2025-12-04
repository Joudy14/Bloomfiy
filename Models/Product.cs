using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bloomfiy.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("name")]
        public string Name { get; set; }

        // Helper property to get image folder name (lowercase, no spaces)
        [NotMapped]
        public string ImageFolderName
        {
            get
            {
                if (string.IsNullOrEmpty(this.Name))
                    return "default";

                // Convert to lowercase, remove spaces, special characters
                var folderName = this.Name.ToLower();
                folderName = System.Text.RegularExpressions.Regex.Replace(folderName, @"[^a-z0-9]", "");
                return folderName;
            }
        }

        [StringLength(500)]
        [Column("description")]
        public string Description { get; set; }

        [Required]
        [Column("base_price")]
        public decimal BasePrice { get; set; }

        [Column("stock_quantity")]
        public int StockQuantity { get; set; } = 0;

        [Column("category_id")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Categories Category { get; set; } // Note: Categories (plural) not Category}

        [Column("is_available")]
        public bool IsAvailable { get; set; } = true;

        [StringLength(100)]
        [Column("info_image")]
        public string InfoImage { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        // Navigation property for ProductColors
        public virtual ICollection<ProductColor> ProductColors { get; set; }

        // Helper property to get default image
        [NotMapped]
        public string DefaultImageUrl
        {
            get
            {
                if (this.ProductColors != null && this.ProductColors.Count > 0)
                {
                    foreach (var pc in this.ProductColors)
                    {
                        if (pc != null && !string.IsNullOrEmpty(pc.ImageFileName))
                        {
                            return $"/Images/products_img/{this.ImageFolderName}/{pc.ImageFileName}";
                        }
                    }
                }
                return "/Images/products_img/default.jpg";
            }
        }

        [NotMapped]
        public string InfoImageUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(this.InfoImage))
                {
                    return $"/Images/products_img/{this.ImageFolderName}/{this.InfoImage}";
                }
                // Fallback to first color's product image
                if (this.ProductColors != null && this.ProductColors.Count > 0)
                {
                    foreach (var pc in this.ProductColors)
                    {
                        if (pc != null && !string.IsNullOrEmpty(pc.ImageFileName))
                        {
                            return $"/Images/products_img/{this.ImageFolderName}/{pc.ImageFileName}";
                        }
                    }
                }
                return "/Images/products_img/default.jpg";
            }
        }
    }
}