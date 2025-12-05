using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bloomfiy.Models;

namespace Bloomfiy.Controllers
{
    public class CartController : Controller
    {

        // Show cart
        public ActionResult Index()
        {
            System.Diagnostics.Debug.WriteLine("CartController.Index hit!");
            var cart = Session["Bloomfiy.Cart"] as List<Bloomfiy.Models.CartItem> ?? new List<Bloomfiy.Models.CartItem>();
            return View(cart);
        }

        private const string SESSION_CART = "Bloomfiy.Cart";

        // Add to cart
        [HttpPost]
        public ActionResult Add(int id, string name, decimal price, string color, int quantity)
        {
            var cart = Session[SESSION_CART] as List<CartItem> ?? new List<CartItem>();

            // Check if same product + color exists
            var item = cart.FirstOrDefault(x => x.ProductId == id && x.Color == color);
            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = id,
                    Name = name,
                    Price = price,
                    Color = color,
                    Quantity = quantity
                });
            }

            Session[SESSION_CART] = cart;
            return RedirectToAction("Index");
        }

        // Remove item
        public ActionResult Remove(int id, string color)
        {
            var cart = Session[SESSION_CART] as List<CartItem> ?? new List<CartItem>();
            var item = cart.FirstOrDefault(x => x.ProductId == id && x.Color == color);
            if (item != null)
                cart.Remove(item);

            Session[SESSION_CART] = cart;
            return RedirectToAction("Index");
        }

        // Clear cart
        public ActionResult Clear()
        {
            Session[SESSION_CART] = null;
            return RedirectToAction("Index");
        }

        // Confirm cart -> goes to Checkout page
        public ActionResult Confirm()
        {
            var cart = Session[SESSION_CART] as List<CartItem> ?? new List<CartItem>();
            if (!cart.Any())
            {
                TempData["Message"] = "Your cart is empty!";
                return RedirectToAction("Index");
            }

            // Save cart for checkout
            Session["CheckoutCart"] = cart;

            // Clear cart
            Session[SESSION_CART] = null;

            return RedirectToAction("Index", "Checkout"); // Redirect to Checkout page
        }
    }
}
