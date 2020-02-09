using Survey.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Survey.Controllers
{
    public class StefiController : Controller
    {
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

                if (startDate < DateTime.Now.Date || startDate > endDate)
                {
                    return RedirectToAction("InvalidDate");
                }

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




    }
}
