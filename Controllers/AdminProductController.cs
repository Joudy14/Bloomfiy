using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Bloomfiy.Models;

namespace Bloomfiy.Controllers
{
    public class AdminProductController : Controller
    {
        // In-memory product list
        private static List<Product> products = new List<Product>();
        private static int nextId = 1;

        // GET: /AdminProduct/
        public ActionResult Index()
        {
            return View("~/Views/Admin/AdminProduct/Index.cshtml", products);
        }

        // GET: /AdminProduct/Create
        public ActionResult Create()
        {
            return View("~/Views/Admin/AdminProduct/Create.cshtml");
        }

        // POST: /AdminProduct/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.ProductId = nextId++;
                product.DateCreated = DateTime.Now;

                // Handle file upload
                var file = Request.Files["ImageFile"];
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string folderPath = Server.MapPath("~/Images/products_img/");

                    // Create folder if it doesn't exist
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string filePath = Path.Combine(folderPath, fileName);
                    file.SaveAs(filePath);
                    product.ImageUrl = "/Images/products_img/" + fileName;
                }
                else
                {
                    product.ImageUrl = "/Images/products_img/default.jpg";
                }

                products.Add(product);
                TempData["SuccessMessage"] = "Product created successfully!";
                return RedirectToAction("Index");
            }

            return View("~/Views/Admin/AdminProduct/Create.cshtml", product);
        }

        // GET: /AdminProduct/Edit/5
        public ActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/AdminProduct/Edit.cshtml", product);
        }

        // POST: /AdminProduct/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (existingProduct != null)
                {
                    // Handle file upload
                    var file = Request.Files["ImageFile"];
                    if (file != null && file.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string folderPath = Server.MapPath("~/Images/products_img/");
                        string filePath = Path.Combine(folderPath, fileName);
                        file.SaveAs(filePath);
                        existingProduct.ImageUrl = "/Images/products_img/" + fileName;
                    }

                    // Update properties
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.BasePrice = product.BasePrice;  // This should work now
                    existingProduct.StockQuantity = product.StockQuantity;
                    existingProduct.IsAvailable = product.IsAvailable;

                    TempData["SuccessMessage"] = "Product updated successfully!";
                    return RedirectToAction("Index");
                }
            }
            return View("~/Views/Admin/AdminProduct/Edit.cshtml", product);
        }

        // GET: /AdminProduct/Delete/5
        public ActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
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
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product != null)
            {
                products.Remove(product);
                TempData["SuccessMessage"] = "Product deleted successfully!";
            }
            return RedirectToAction("Index");
        }

        // Simple test action
        public string Test()
        {
            return "AdminProductController is working!";
        }

        public ActionResult SimpleTest()
        {
            try
            {
                var db = new ApplicationDbContext();
                var product = new Product
                {
                    Name = "Test Product",
                    Description = "Test Description",
                    BasePrice = 19.99m,  // Use BasePrice
                    StockQuantity = 10,
                    ImageUrl = "/Images/products_img/default.jpg",
                    CategoryId = 1,
                    IsAvailable = true
                };

                db.Products.Add(product);
                db.SaveChanges();

                return Content($"✅ Test passed! Product added with BasePrice: ${product.BasePrice}");
            }
            catch (Exception ex)
            {
                return Content($"❌ Error: {ex.Message}");
            }
        }
    }
}