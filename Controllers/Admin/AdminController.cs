using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bloomfiy.Models;

namespace Bloomfiy.Controllers.Admin
{
    public class AdminController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminLoginModel model)
        {
            // Admin login logic
            return RedirectToAction("Dashboard");
        }
    }
}