using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_Group_Project.Models;

namespace Final_Group_Project.Controllers
{
    public class HomeController : Controller
    {
        ScoresDBEntities1 db;
        public HomeController()
        {
            db = new ScoresDBEntities1();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewData.Model = db.Scores.ToList();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}