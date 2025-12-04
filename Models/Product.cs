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

            [Column("category_id")]
            public int CategoryId { get; set; }

            [ForeignKey("CategoryId")]
            public virtual Category Category { get; set; }

            [Column("is_available")]
            public bool IsAvailable { get; set; } = true;

            [Column("date_created")]
            public DateTime DateCreated { get; set; } = DateTime.Now;

            // Navigation property for ProductColors (junction table)
            public virtual ICollection<ProductColor> ProductColors { get; set; }

            // Helper property to get default image (first color's image)
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

            // Helper property for short description
            [NotMapped]
            public string ShortDescription
            {
                get
                {
                    if (string.IsNullOrEmpty(Description))
                        return string.Empty;

                    if (Description.Length > 100)
                        return Description.Substring(0, 100) + "...";

                    return Description;
                }
            }
        }
    }