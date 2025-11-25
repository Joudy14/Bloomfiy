using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bloomfiy.Controllers.Admin
{
    public class AdminEventController : Controller
    {
        public ActionResult Bookings()
        {
            return View();
        }

        public ActionResult BookingDetails(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateStatus(int id, string status)
        {
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult CancelBooking(int id)
        {
            return Json(new { success = true });
        }
    }
}