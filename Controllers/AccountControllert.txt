using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bloomfiy.Models;

namespace Bloomfiy.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult Addresses()
        {
            return View();
        }

        public ActionResult Wishlist()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            // Login logic here
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            // Registration logic here
            return RedirectToAction("Dashboard");
        }

        public ActionResult Logout()
        {
            // Logout logic here
            return RedirectToAction("Index", "Home");
        }
    }
}