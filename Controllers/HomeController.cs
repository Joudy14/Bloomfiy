using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bloomfiy.Models;
using System.Web.Mvc;
using System.Data.Entity;
using System.Text;

namespace Bloomfiy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

      
       }
    }