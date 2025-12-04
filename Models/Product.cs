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

    [StringLength(500)]
    [Column("description")]
    public string Description { get; set; }

    [Required]
    [Column("base_price")]
    public decimal BasePrice { get; set; }

    [Column("stock_quantity")]
    public int StockQuantity { get; set; } = 0;

        // In Product.cs, add this property after ImageUrl:
        [StringLength(500)]
        [Column("image_url")]
        public string ImageUrl { get; set; }

        // Add this property (optional but useful for multiple images)
        [NotMapped]
        public string MainImageUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(ImageUrl))
                    return ImageUrl;

                // Fallback based on product name or category
                if (Name.ToLower().Contains("peony"))
                    return "/Images/products_img/peony/peony_pink.png";
                if (Name.ToLower().Contains("rose"))
                    return "/Images/products_img/roses/rose_red.png";

                return "/Images/default-flower.jpg";
            }
        }

        [NotMapped]
        public string[] AdditionalImageUrls
        {
            get
            {
                if (Name.ToLower().Contains("peony"))
                    return new string[]
                    {
                "/Images/products_img/peony/peony_pink.png",
                "/Images/products_img/peony/peony_red.png",
                "/Images/products_img/peony/peony_white.png"
                    };

                return new string[] { MainImageUrl };
            }
        }

        [Column("category_id")]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }

    [Column("is_available")]
    public bool IsAvailable { get; set; } = true;

    [Column("date_created")]
    public DateTime DateCreated { get; set; } = DateTime.Now;

    // ✅ CORRECT: Navigation property for Colors (many-to-many via ProductColors)
    public virtual ICollection<ProductColor> ProductColors { get; set; }
}

}