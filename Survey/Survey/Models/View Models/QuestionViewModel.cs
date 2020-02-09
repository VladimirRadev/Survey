using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Survey.Models.View_Models
{
    public class QuestionViewModel
    {
        public int SelectedID { get; set; }

        public Question QuestionForEdit { get; set; }

        public String Answer1 { get; set; }

        public String Answer2 { get; set; }
    }
}