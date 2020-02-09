using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Models
{
    public partial class Question
    {
        public Question(string question_text, System.DateTime start_date, System.DateTime end_date)
        {
            this.Answers = new HashSet<Answer>();
            this.Question_text = question_text;
            this.Start_date = start_date;
            this.End_date = end_date;
            this.Is_deleted = false;
        }
    }
}