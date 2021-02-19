using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using _5204_passion_project_n01446543.Models;

namespace _5204_passion_project_n01446543.Controllers
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


        // on login redirect to user dashboard
        public ActionResult Dashboard(LoginViewModel model)
        {
            ViewBag.Message = "DashBoard";

            return View(model);
        }
    }
}