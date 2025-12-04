using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bloomfiy.Models;

namespace Bloomfiy.Controllers.Admin
{
    public class AdminCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminCategory
        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View("~/Views/Admin/AdminCategory/Index.cshtml", categories);
        }

        // GET: AdminCategory/Create
        public ActionResult Create()
        {
            return View("~/Views/Admin/AdminCategory/Create.cshtml");
        }

        // POST: AdminCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categories category) // Changed from CategoryModel to Category
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Category created successfully!";
                return RedirectToAction("Index");
            }
            return View("~/Views/Admin/AdminCategory/Create.cshtml", category);
        }

        // GET: AdminCategory/Edit/5
        public ActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/AdminCategory/Edit.cshtml", category);
        }

        // POST: AdminCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categories category) // Changed from CategoryModel to Category
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }
            return View("~/Views/Admin/AdminCategory/Edit.cshtml", category);
        }

        // POST: AdminCategory/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            if (category != null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Category deleted successfully!";
            }
            return RedirectToAction("Index");
        }
    }
}