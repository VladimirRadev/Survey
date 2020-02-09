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
    public class RusanaController : Controller
    {
        private Question quest;

        public ActionResult RusanaView(FormCollection data)
        {
            int questionId = Convert.ToInt32(data["SelectedID"]);


            ViewData["idTest"] = questionId;

            return View();

        }


    }
}
