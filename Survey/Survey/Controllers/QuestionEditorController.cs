using Survey.Models;
using Survey.Models.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Survey.Controllers {


    public class QuestionEditorController : Controller {

        public ActionResult QuestionEditor() {
            return View();
        }


        //We use q4Edit to hold the current object
        Question q4Edit;
        public ActionResult qDelete() {
            int qID = 2;
            q4Edit = DataProvider.GetQuestion(qID);
            q4Edit.Is_deleted = true;
            DataProvider.EditQuestion(qID, q4Edit);
            return View();

        }
        public ActionResult qTextEdit() {
            //HARDCODE: qID, newQuestion -> Temp Hardcode Values (No Interface)
            int qID = 2;
            string newQuestion = "this is my new question";
            q4Edit = DataProvider.GetQuestion(qID);
            q4Edit.Question_text = newQuestion;
            DataProvider.EditQuestion(qID, q4Edit);
            return View(); 
        }
        public ActionResult qDateStartEdit() {
            int qID = 2;
            //Temporary DateTime (no interface)
            DateTime currDate = new DateTime(2019, 5, 7, 16, 31, 0);
            q4Edit = DataProvider.GetQuestion(qID);
            q4Edit.Start_date = currDate;
            DataProvider.EditQuestion(qID, q4Edit);
            return View();
        }
        public ActionResult qDateEndEdit() {
            int qID = 2;
            //Temporary DateTime (no interface)
            DateTime currDate = new DateTime(2019, 5, 7, 16, 31, 0);
            q4Edit = DataProvider.GetQuestion(qID);
            q4Edit.End_date = currDate;
            DataProvider.EditQuestion(qID, q4Edit);
            return View();
        }

        //Response.Write("<script>alert(" + textBox + ")</script>");
        //Response.Write("<script>alert('AAAAAAAAAA')</script>");
    }


}
