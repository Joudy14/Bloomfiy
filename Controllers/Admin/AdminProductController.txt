using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bloomfiy.Models;

namespace Bloomfiy.Controllers.Admin
{
    public class AdminProductController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductModel model)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ProductModel model)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return Json(new { success = true });
        }
    }
}