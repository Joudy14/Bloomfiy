using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bloomfiy.Models;
using System.Data.Entity;

namespace Bloomfiy.Controllers
{
    public class AdminProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /AdminProduct/
        public ActionResult Index()
        {
            var products = db.Products
                .Include("Category")
                .Include("ProductColors")
                .ToList();
            return View("~/Views/Admin/AdminProduct/Index.cshtml", products);
        }

        // GET: /AdminProduct/Create
        public ActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Colors = db.Colors.Where(c => c.IsAvailable).ToList();
            return View("~/Views/Admin/AdminProduct/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, int[] selectedColors,
                                   Dictionary<int, string> colorImageNames,
                                   string productInfoImage)
        {
            // DEBUG: Log everything
            System.Diagnostics.Debug.WriteLine($"=== CREATE DEBUG ===");
            System.Diagnostics.Debug.WriteLine($"CategoryId from form: {product.CategoryId}");
            System.Diagnostics.Debug.WriteLine($"All categories in DB:");
            foreach (var cat in db.Categories.ToList())
            {
                System.Diagnostics.Debug.WriteLine($"- ID: {cat.CategoryId}, Name: {cat.CategoryName}");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Double-check category exists
                    var category = db.Categories.Find(product.CategoryId);
                    if (category == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"ERROR: CategoryId {product.CategoryId} not found!");
                        ModelState.AddModelError("CategoryId", "Selected category does not exist");
                        ViewBag.Categories = db.Categories.ToList();
                        ViewBag.Colors = db.Colors.Where(c => c.IsAvailable).ToList();
                        return View("~/Views/Admin/AdminProduct/Create.cshtml", product);
                    }

                    System.Diagnostics.Debug.WriteLine($"Found category: {category.CategoryName}");

                    product.DateCreated = DateTime.Now;
                    product.InfoImage = productInfoImage;

                    // Save product
                    db.Products.Add(product);
                    db.SaveChanges();

                    // 2. Create folder for images
                    string folderPath = Server.MapPath($"~/Images/products_img/{product.ImageFolderName}/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // 3. Save colors with images
                if (selectedColors != null)
                {
                    foreach (var colorId in selectedColors)
                    {
                        var productColor = new ProductColor
                        {
                            ProductId = product.ProductId,
                            ColorId = colorId,
                            ImageFileName = colorImageNames.ContainsKey(colorId)
                                ? colorImageNames[colorId].Trim()
                                : $"{product.ImageFolderName}_{colorId}.jpg"
                        };

                        db.ProductColors.Add(productColor);
                    }
                    db.SaveChanges();
                }

                    TempData["SuccessMessage"] = "Product created successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"EXCEPTION: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"INNER: {ex.InnerException.Message}");
                    }
                    ViewBag.Error = ex.Message;
                    ViewBag.Categories = db.Categories.ToList();
                    ViewBag.Colors = db.Colors.Where(c => c.IsAvailable).ToList();
                    return View("~/Views/Admin/AdminProduct/Create.cshtml", product);
                }
            }

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Colors = db.Colors.Where(c => c.IsAvailable).ToList();
            return View("~/Views/Admin/AdminProduct/Create.cshtml", product);
        }


        // GET: /AdminProduct/Edit/5
        public ActionResult Edit(int id)
        {
            var product = db.Products
                .Include("ProductColors")
                .Include("ProductColors.Color")
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Colors = db.Colors.Where(c => c.IsAvailable).ToList();

            return View("~/Views/Admin/AdminProduct/Edit.cshtml", product);
        }

        // POST: /AdminProduct/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, int[] selectedColors, Dictionary<int, string> colorImageNames, string productInfoImage)
        {
            if (ModelState.IsValid)
            {
                // Update product basic info
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                // Get existing product colors
                var existingProductColors = db.ProductColors
                    .Where(pc => pc.ProductId == product.ProductId)
                    .ToList();

                // Create a dictionary of existing color-image pairs
                var existingImages = new Dictionary<int, string>();
                foreach (var pc in existingProductColors)
                {
                    existingImages[pc.ColorId] = pc.ImageFileName;
                }

                // Remove all existing colors
                db.ProductColors.RemoveRange(existingProductColors);
                db.SaveChanges();

                // Add updated colors with their image filenames
                if (selectedColors != null)
                {
                    for (int i = 0; i < selectedColors.Length; i++)
                    {
                        var productColor = new ProductColor
                        {
                            ProductId = product.ProductId,
                            ColorId = selectedColors[i],
                            ImageFileName = colorImageNames != null && i < colorImageNames.Count && !string.IsNullOrEmpty(colorImageNames[i].Trim())
                                ? colorImageNames[i].Trim()
                                : existingImages.ContainsKey(selectedColors[i])
                                    ? existingImages[selectedColors[i]]
                                    : $"{product.ImageFolderName}_{i + 1}.jpg"
                        };

                        db.ProductColors.Add(productColor);
                    }
                    db.SaveChanges();
                }

                TempData["SuccessMessage"] = "Product updated successfully!";
                return RedirectToAction("Index");
            }

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Colors = db.Colors.Where(c => c.IsAvailable).ToList();
            return View("~/Views/Admin/AdminProduct/Edit.cshtml", product);
        }

        // GET: /AdminProduct/Delete/5
        public ActionResult Delete(int id)
        {
            var product = db.Products
                .Include("ProductColors")
                .Include("ProductColors.Color")
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/AdminProduct/Delete.cshtml", product);
        }

        // POST: /AdminProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                // First delete related ProductColors
                var relatedColors = db.ProductColors.Where(pc => pc.ProductId == id).ToList();
                db.ProductColors.RemoveRange(relatedColors);

                // Then delete product
                db.Products.Remove(product);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Product deleted successfully!";
            }
            return RedirectToAction("Index");
        }

        // Toggle Availability
        public ActionResult ToggleAvailability(int id)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                product.IsAvailable = !product.IsAvailable;
                db.SaveChanges();
                TempData["SuccessMessage"] = $"Product {(product.IsAvailable ? "activated" : "deactivated")} successfully!";
            }
            return RedirectToAction("Index");
        }
    }
}