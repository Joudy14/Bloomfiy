using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bloomfiy.Models;

namespace Bloomfiy.Controllers
{
    public class AdminProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /AdminProduct/
        public ActionResult Index()
        {
            var products = db.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColors)
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

        // POST: /AdminProduct/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, int[] selectedColors, HttpPostedFileBase[] colorImages)
        {
            if (ModelState.IsValid)
            {
                product.DateCreated = DateTime.Now;
                db.Products.Add(product);
                db.SaveChanges();

                // Add colors with their images
                if (selectedColors != null)
                {
                    for (int i = 0; i < selectedColors.Length; i++)
                    {
                        var productColor = new ProductColor
                        {
                            ProductId = product.ProductId,
                            ColorId = selectedColors[i]
                        };

                        // Handle image upload for this color
                        if (colorImages != null && i < colorImages.Length && colorImages[i] != null && colorImages[i].ContentLength > 0)
                        {
                            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(colorImages[i].FileName);
                            string folderPath = Server.MapPath("~/Images/products_img/");

                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            string filePath = Path.Combine(folderPath, fileName);
                            colorImages[i].SaveAs(filePath);
                            productColor.ImageUrl = "/Images/products_img/" + fileName;
                        }
                        else
                        {
                            productColor.ImageUrl = "/Images/default-flower.jpg";
                        }

                        db.ProductColors.Add(productColor);
                    }
                    db.SaveChanges();
                }

                TempData["SuccessMessage"] = "Product created successfully!";
                return RedirectToAction("Index");
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
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Colors = db.Colors.Where(c => c.IsAvailable).ToList();

            // Load color data
            foreach (var pc in product.ProductColors)
            {
                db.Entry(pc).Reference(p => p.Color).Load();
            }

            return View("~/Views/Admin/AdminProduct/Edit.cshtml", product);
        }

        // POST: /AdminProduct/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, int[] selectedColors, HttpPostedFileBase[] colorImages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                // Remove existing colors
                var existingColors = db.ProductColors.Where(pc => pc.ProductId == product.ProductId).ToList();
                db.ProductColors.RemoveRange(existingColors);
                db.SaveChanges();

                // Add new colors with images
                if (selectedColors != null)
                {
                    for (int i = 0; i < selectedColors.Length; i++)
                    {
                        var productColor = new ProductColor
                        {
                            ProductId = product.ProductId,
                            ColorId = selectedColors[i]
                        };

                        // Handle image upload
                        if (colorImages != null && i < colorImages.Length && colorImages[i] != null && colorImages[i].ContentLength > 0)
                        {
                            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(colorImages[i].FileName);
                            string folderPath = Server.MapPath("~/Images/products_img/");
                            string filePath = Path.Combine(folderPath, fileName);
                            colorImages[i].SaveAs(filePath);
                            productColor.ImageUrl = "/Images/products_img/" + fileName;
                        }
                        else
                        {
                            productColor.ImageUrl = "/Images/default-flower.jpg";
                        }

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
            var product = db.Products.Find(id);
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

        // Test action
        public ActionResult Test()
        {
            try
            {
                // Create test data if needed
                if (!db.Categories.Any())
                {
                    db.Categories.Add(new Category { CategoryName = "Bouquets" });
                    db.SaveChanges();
                }

                if (!db.Colors.Any())
                {
                    db.Colors.AddRange(new List<Color>
                    {
                        new Color { ColorName = "Red", ColorCode = "#FF0000" },
                        new Color { ColorName = "White", ColorCode = "#FFFFFF" },
                        new Color { ColorName = "Pink", ColorCode = "#FFC0CB" }
                    });
                    db.SaveChanges();
                }

                return Content("✅ AdminProductController is working!");
            }
            catch (Exception ex)
            {
                return Content($"❌ Error: {ex.Message}");
            }
        }
    }
}