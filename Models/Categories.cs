using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bloomfiy.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("category_name")]
        public string CategoryName { get; set; }

        [StringLength(200)]
        [Column("description")]
        public string Description { get; set; }

        [StringLength(500)]
        [Column("image_url")]
        public string ImageUrl { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }

}