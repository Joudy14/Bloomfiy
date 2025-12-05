using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bloomfiy.Models
{
    public class Order
    {
        public int Id { get; set; } // assigned when saved to session list
        public string OrderNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Shipping { get; set; }
        public decimal Total { get; set; }

        // buyer info
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string ShippingAddress { get; set; }
    }
}