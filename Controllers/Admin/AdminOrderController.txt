using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bloomfiy.Controllers.Admin
{
    public class AdminOrderController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateStatus(int id, string status)
        {
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Cancel(int id)
        {
            return Json(new { success = true });
        }
    }
}