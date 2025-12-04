using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bloomfiy.Models;
using System.Web.Mvc;
using System.Data.Entity; 

namespace Bloomfiy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult TestEF()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    // FIXED: Correct Include syntax for nested collections
                    var products = db.Products
                        .Include(p => p.Category)
                        .Include(p => p.ProductColors)  // ✅ Correct syntax
                        .ToList();  // Load products first

                    var categories = db.Categories.ToList();
                    var colors = db.Colors.ToList();

                    string result = "<h3>✅ Entity Framework Connected!</h3>";
                    result += $"<p>Products: {products.Count}</p>";
                    result += $"<p>Categories: {categories.Count}</p>";
                    result += $"<p>Colors: {colors.Count}</p>";

                    // Show products with colors
                    result += "<h4>Products from Database:</h4><ul>";

                    foreach (var product in products)
                    {
                        result += $"<li><strong>{product.Name}</strong> - ${product.BasePrice}";

                        // Load Color for each ProductColor
                        if (product.ProductColors != null)
                        {
                            foreach (var pc in product.ProductColors)
                            {
                                // Explicitly load the Color for each ProductColor
                                db.Entry(pc).Reference(p => p.Color).Load();
                            }

                            if (product.ProductColors.Any())
                            {
                                result += " (Colors: ";
                                // Get Color names via the ProductColors junction
                                var colorNames = product.ProductColors
                                    .Select(pc => pc.Color?.ColorName)
                                    .Where(name => !string.IsNullOrEmpty(name));
                                result += string.Join(", ", colorNames);
                                result += ")";
                            }
                        }
                        result += "</li>";
                    }
                    result += "</ul>";

                    return Content(result);
                }
            }
            catch (Exception ex)
            {
                return Content($"❌ Entity Framework Error: {ex.Message}<br><br>{ex.StackTrace}");
            }
        }
    }
}