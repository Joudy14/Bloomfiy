using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bloomfiy.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Confirmation(int id)
        {
            return View();
        }

        public ActionResult History()
        {
            return View();
        }

        public ActionResult Tracking(int id)
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cancel(int id)
        {
            return Json(new { success = true });
        }
    }
}