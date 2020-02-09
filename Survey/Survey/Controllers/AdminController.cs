using Survey.Models;
using Survey.Models.View_Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Survey.Controllers
{

    public class AdminController : Controller
    {

        private Question quest;

        // GET: Admin/Panel
        public ActionResult Panel()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }
        public ActionResult DeleteQuestion(FormCollection data)
        {
            int questionId = Convert.ToInt32(data["SelectedID"]);
            DataProvider.DeleteQuestion(questionId);
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult SaveQuestion(FormCollection data)
        {
            try
            {
                if (String.IsNullOrEmpty(data["question"]) || data["question"].Length >= 255)
                {
                    return RedirectToAction("InvalidQuestionText");
                }
                if (String.IsNullOrEmpty(data["startDate"]) || String.IsNullOrEmpty(data["endDate"]))
                {
                    return RedirectToAction("InvalidDate");
                }

                string questionText = data["question"];
                DateTime startDate = DateTime.ParseExact(data["startDate"], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(data["endDate"], "yyyy-MM-dd", CultureInfo.InvariantCulture);

                

                //Response.Write("questionText: " + questionText);
                //Response.Write("startDate: " + startDate);
                //Response.Write("endDate: " + endDate);

                Question question = new Question(questionText, startDate, endDate);
                DataProvider.InsertQuestion(question);

                if (!String.IsNullOrEmpty(data["answer1"]))
                {
                    OriginalAnswer answer = new OriginalAnswer(data["answer1"], question);
                    DataProvider.InsertOriginalAnswer(answer);
                }

                if (!String.IsNullOrEmpty(data["answer2"]))
                {
                    OriginalAnswer answer = new OriginalAnswer(data["answer2"], question);
                    DataProvider.InsertOriginalAnswer(answer);
                }

                return RedirectToAction("SuccessfulQuestionSaving");
                //return View();
            }
            catch (Exception)
            {
                return RedirectToAction("UnsuccessfulQuestionSaving");
            }
        }


        public ActionResult InvalidQuestionText()
        {
            return View();
        }

        public ActionResult InvalidDate()
        {
            return View();
        }

        public ActionResult SuccessfulQuestionSaving()
        {
            return View();
        }

        public ActionResult UnsuccessfulQuestionSaving()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoadQuestionDetails(QuestionViewModel data)
        {
            data.QuestionForEdit = DataProvider.GetQuestion(data.SelectedID);
            var answers = DataProvider.GetOriginalAnswers(data.SelectedID);
            if (answers != null && answers.Count > 0)
            {
                if (answers.Count == 1)
                {
                    data.Answer1 = answers[0].Answer_text;
                }
                if (answers.Count == 2)
                {
                    data.Answer1 = answers[0].Answer_text;
                    data.Answer2 = answers[1].Answer_text;
                }
            }
            return View("Edit", data);

        }

        [HttpPost]
        public ActionResult UpdatedQuestion(QuestionViewModel data)
        {
            //All edited data is in data object

            //Response.Write(data.SelectedID);

            //DataProvider.EditQuestion(data.SelectedID, data.QuestionForEdit);
            //DataProvider.EditOriginalAnswers(data.SelectedID, data.Answer1, data.Answer2);

            return View("Panel");
        }



        // POST: Admin/AddAnswers
        [HttpPost]
        public ActionResult AddAnswers(CreateQuestionViewModel model)
        {
            try
            {
                // TODO: Add insert logic here しのぶ kek

                String question = model.QuestionText;
                String[] answers = model.RawAnswers.Split(';');

                Response.Write(question);
                //Response.Write(answers[0]);

                Question temp = new Models.Question(question, new DateTime(2019, 7, 5), new DateTime(2019, 7, 6));
                DataProvider.InsertQuestion(temp);

                foreach (String answer in answers)
                {
                    DataProvider.InsertOriginalAnswer(new OriginalAnswer(answer, temp));
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ShowStats()
        {
            return View();
        }

        public ActionResult Statistic(FormCollection data)
        {
            //data["type"]
            //gender - спрямо пол
            //experience - спрямо стаж
            int questionId = Convert.ToInt32(data["SelectedID"]);
            ViewData["idTest"] = questionId;


            List<Question> questions = DataProvider.GetAllQuestions().Where(q => q.Id == questionId).ToList();
            quest = questions[0];
            List<Answer> aswersOfquest = DataProvider.GetAllAnswers(quest.Id);
            List<Answer> answersOfMens = aswersOfquest.Where(a => a.Gender == "m").ToList();
            List<Answer> answerOfWomans = aswersOfquest.Where(a => a.Gender == "f").ToList();
            List<Answer> answerOfPersonsWorksBetween0and5 = aswersOfquest.Where(a => a.Experience == "0-5").ToList();
            List<Answer> answerOfPersonsWorksBetween5and10 = aswersOfquest.Where(a => a.Experience == "5-10").ToList();
            List<Answer> answerOfPersonsWorksBetween10nd15 = aswersOfquest.Where(a => a.Experience == "10-15").ToList();
            List<Answer> answerOfPersonsWork15plus = aswersOfquest.Where(a => a.Experience == "15+").ToList();


            //**************************************************************************
            //Infos to send to statistics html and create gender chart

            int mens = answersOfMens.Count;
            int womens = answerOfWomans.Count;



            //*******************************************
            //Infos to send to statistics html and create experience chart
            int zeroToFive = answerOfPersonsWorksBetween0and5.Count;
            int fiveToTen = answerOfPersonsWorksBetween10nd15.Count;
            int tenToFifteen = answerOfPersonsWorksBetween10nd15.Count;
            int fifteenPLUS = answerOfPersonsWork15plus.Count;


            if (data["type"] == "gender")
            {
                var gender1 = new Chart(width: 600, height: 400)
                           .AddTitle($"{quest.Question_text}")
                         .AddSeries(
                          name: "Employee",
                           xValue: new[] { "Mъже", "Жени" },
                             yValues: new[] { $"{mens}", $"{womens}" }).Write();
                ViewData["statisricType"] = "Статистика спрямо пол";
                ViewData["stats"] = gender1;

            }
            else if (data["type"] == "experience")
            {
                var experience1 = new Chart(width: 600, height: 400)
                 .AddTitle($"{quest.Question_text}")
                .AddSeries(
                name: "Employee",
                xValue: new[] { "0-5", "5-10", "10-15", "15+" },
                yValues: new[] { $"{zeroToFive}", $"{fiveToTen}", $"{tenToFifteen}", $"{fifteenPLUS}" }).Write();
                ViewData["statisricType"] = "Статистика спрямо стаж";
                ViewData["stats"] = experience1;

            }

            //  Response.Write("question: " + data["question"]);

            return View();
        }
    }
}