using Survey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Survey.Controllers
{
    public class HomeController : Controller
    {

        private static Question question;
        private static string answer;
        private static string gender;
        private static string experience;

        public ActionResult Index()
        {

            List<Question> questions = DataProvider.GetActiveQuestions();
            Random random = new Random();
            question = questions[random.Next(0, questions.Count)];
            ViewData["question"] = question.Question_text;
            ViewData["answers"] = DataProvider.GetOriginalAnswers(question.Id).ToList();
            return View();
        }

        public ActionResult DataSubmit(FormCollection data)
        {
            if (!String.IsNullOrEmpty(data["Answer"]))
            {
                answer = data["Answer"];
            }
            else if (!String.IsNullOrEmpty(data["textBox"]))
            {
                if (data["Answer"].Length >= 255)
                {
                    return RedirectToAction("TooLongAnswer");
                }

                answer = data["textBox"];
            }
            else
            {
                return RedirectToAction("InvalidInput");
            }
            //Response.Write("answer: " + answer);

            return View();
        }

        public ActionResult TooLongAnswer()
        {
            return View();
        }

        public ActionResult InvalidInput()
        {
            return View();
        }

        public ActionResult PersonalDataSubmit(FormCollection data)
        {
            if (String.IsNullOrEmpty(data["gender"]) || String.IsNullOrEmpty(data["experience"]))
            {
                return RedirectToAction("InvalidPersonalData");
            }

            if (data["gender"] == "Мъж")
            {
                gender = "m";
            }
            else
            {
                gender = "f";
            }

            if (data["experience"] == "0-5 години")
            {
                experience = "0-5";
            }
            else if (data["experience"] == "5-10 години")
            {
                experience = "5-10";
            }
            else if (data["experience"] == "10-15 години")
            {
                experience = "10-15";
            }
            else
            {
                experience = "15+";
            }

            //Response.Write("answer: " + answer);
            //Response.Write("gender: " + gender);
            //Response.Write("experience: " + experience);
            //Response.Write("question.Question_text: " + question.Question_text);

            clientSubmit();

            return View();
        }

        public ActionResult InvalidPersonalData()
        {
            return View();
        }
        public ActionResult TestStatsView()
        {
            List<Question> questions = DataProvider.GetActiveQuestions();
            Random random = new Random();
            question = questions[random.Next(0, questions.Count)];
            ViewData["question"] = question.Question_text;
            ViewData["answers"] = DataProvider.GetOriginalAnswers(question.Id).ToList();
            return View();
        }

        //poluchawa informaciq ot parwiq ekran za podawane na otgowor (info ot radio butonite - dadeni otg)
        public static void clientSubmit()
        {
            DataProvider.InsertAnswerFromPerson(new Answer(answer, gender, experience, question));
        }
    }
}