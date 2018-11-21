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

        public ActionResult High_Score()
        {
            
            ViewData.Model = db.Scores.ToList();
            return View();
        }

        public ActionResult Search(string search)
        {
            var searchResult = db.Scores.ToList();
            if (search != null)
            {
                foreach (var scores in searchResult)
                {
                    if (!scores.PlayerName.Contains(search))
                    {
                        searchResult.Remove(scores);
                    }
                }
                ViewData.Model = searchResult.ToList();
            }
            
            return View();
        }
    }
}