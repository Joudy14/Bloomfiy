using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bloomfiy.Controllers
{
    public class CareGuideController : Controller
    {
        // GET: Care
        public ActionResult Index()
        {
            return View();
        }
        // Page: Views/CareGuide/Details.cshtml
        public ActionResult Details()
        {
            return View();
        }
    }
}