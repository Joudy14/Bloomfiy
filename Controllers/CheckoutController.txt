using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bloomfiy.Controllers
{
    public class CheckoutController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Shipping()
        {
            return View();
        }

        public ActionResult Payment()
        {
            return View();
        }

        public ActionResult Confirm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProcessOrder()
        {
            return Json(new { success = true });
        }
    }
}