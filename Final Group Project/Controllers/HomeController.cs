using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
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

        //found info on how to load xml into mvc here http://www.aspmantra.com/2017/03/read-xml-file-and-display-data-using-asp.net-mvc-aspmantra.html

        public ActionResult Quiz(int id)
        {
            XmlDocument doc = new XmlDocument();
            Question question = new Question();
            string path = Server.MapPath("~/Content/Questions.xml");
            doc.Load(path);
            var node = doc.SelectNodes("/questionList/question")[id - 1];

            question.ID = id;
            question.Title = node["title"].InnerText;
            List<string> list = new List<string>();
            foreach (XmlNode answer in node.ChildNodes)
            {
                if(answer.Name == "answer")
                {
                    list.Add(answer.InnerText);
                }
            }
            question.answers = list;
            return View(question);
            
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