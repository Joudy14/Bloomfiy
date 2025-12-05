using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Bloomfiy.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }

        // Computed property
        public decimal TotalPrice => Price * Quantity;
    }
}
