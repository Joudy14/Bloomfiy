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

        // Store only the FILENAME, not full path
        // Example: "tulip_pink.jpg" or "rose_red.png"
        [StringLength(100)]
        [Column("image_filename")]
        public string ImageFileName { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("ColorId")]
        public virtual Color Color { get; set; }

        // Helper property to get full image URL
        [NotMapped]
        public string ImageUrl
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImageFileName) || this.Product == null)
                    return "/Images/products_img/default.jpg";

                return $"/Images/products_img/{this.Product.ImageFolderName}/{this.ImageFileName}";
            }
        }

        // Helper property to get full price
        [NotMapped]
        public decimal FullPrice
        {
            get
            {
                if (this.Product != null && this.Color != null)
                    return this.Product.BasePrice + this.Color.PriceAdjustment;
                return 0;
            }
        }
    }
}