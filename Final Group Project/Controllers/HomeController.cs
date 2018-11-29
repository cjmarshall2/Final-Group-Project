using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Text.RegularExpressions;
using Final_Group_Project.Models;


namespace Final_Group_Project.Controllers
{
    public class HomeController : Controller
    {
        ScoresDBEntities2 db;
        public HomeController()
        {
            db = new ScoresDBEntities2();
        }

        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Quiz_Complete(string quiz)
        {
            ViewBag.Quiz = quiz;
            return View();
        }

        public ActionResult Save(string quiz, string playerName, int quizScore)
        {
            Scores score = new Scores();
            score.PlayerName = playerName;
            score.QuizScore = quizScore;
            score.QuizType = quiz;
            score.QuizDate = DateTime.Now;

            db.Scores.Add(score);
            var errors = db.GetValidationErrors();
            db.SaveChanges();
            return RedirectToAction("index");

        }
        //found info on how to load xml into mvc here http://www.aspmantra.com/2017/03/read-xml-file-and-display-data-using-asp.net-mvc-aspmantra.html

        public ActionResult Quiz(int id, string quiz)
        {
            XmlDocument doc = new XmlDocument();
            Question question = new Question();
            //load xml document based on which button the user selected
            string path = Server.MapPath("~/Content/" + quiz + ".xml");
            doc.Load(path);
            //find the correct question based on id passed in url
            var node = doc.SelectNodes("/questionList/question")[id - 1];
            //create a c# object out of the xml node
            question.ID = id;
            question.Title = node["title"].InnerText;
            List<string> list = new List<string>();
            foreach (XmlNode answer in node.ChildNodes)
            {
                if(answer.Name == "answer")
                {
                    if (answer.Attributes.GetNamedItem("correctAnswer") != null)
                    {
                        list.Add(answer.InnerText + "correctAnswer");
                    }
                    else
                    {
                        list.Add(answer.InnerText);
                    }
                }
            }
            question.answers = list;
            //preserve the quiz that the user has selected for future questions
            ViewBag.Quiz = quiz;
            
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