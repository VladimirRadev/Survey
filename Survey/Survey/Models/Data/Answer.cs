using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Models
{
    public partial class Answer
    {
        public Answer()
        {

        }

        public Answer(string answer_text, string gender, string experience, Question question)
        {
            this.Answer_text = answer_text;
            this.Gender = gender;
            this.Experience = experience;
            this.Is_deleted = false;
            this.Question = question;
            this.Question_id = question.Id;
        }
    }
}