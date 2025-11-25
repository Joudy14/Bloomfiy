using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bloomfiy.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(int productId, int? colorId, bool includeBouquet = false)
        {
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Update(int cartId, int quantity)
        {
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Remove(int cartId)
        {
            return Json(new { success = true });
        }

        public ActionResult GetCartCount()
        {
            return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);
        }
    }
}