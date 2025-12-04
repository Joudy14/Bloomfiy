using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bloomfiy.Models;
using System.Data.Entity;

namespace Bloomfiy.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Catalog()
        {
            // Get all products with categories and colors
            var products = db.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColors.Select(pc => pc.Color))
                .Where(p => p.IsAvailable)
                .OrderBy(p => p.Name)
                .ToList();

            // Get all categories for the filter
            var categories = db.Categories.ToList();
            var colors = db.Colors.Where(c => c.IsAvailable).ToList();

            ViewBag.Categories = categories;
            ViewBag.Colors = colors;

            return View(products);
        }

        public ActionResult Details(int id)
        {
            var product = db.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColors.Select(pc => pc.Color))
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            // Get related products (same category)
            var relatedProducts = db.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColors.Select(pc => pc.Color))
                .Where(p => p.CategoryId == product.CategoryId && p.ProductId != id && p.IsAvailable)
                .Take(4)
                .ToList();

            ViewBag.RelatedProducts = relatedProducts;
            return View(product);
        }

        public ActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction("Catalog");
            }

            var products = db.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColors.Select(pc => pc.Color))
                .Where(p => p.IsAvailable &&
                      (p.Name.Contains(query) ||
                       p.Description.Contains(query) ||
                       p.Category.CategoryName.Contains(query)))
                .ToList();

            ViewBag.SearchQuery = query;
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Colors = db.Colors.Where(c => c.IsAvailable).ToList();

            return View("Catalog", products);
        }

        public ActionResult ByCategory(int categoryId)
        {
            var products = db.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColors.Select(pc => pc.Color))
                .Where(p => p.IsAvailable && p.CategoryId == categoryId)
                .ToList();

            var category = db.Categories.Find(categoryId);
            ViewBag.CategoryName = category?.CategoryName ?? "Products";
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Colors = db.Colors.Where(c => c.IsAvailable).ToList();

            return View("Catalog", products);
        }
    }
}