using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Models
{
    public partial class OriginalAnswer
    {
        public OriginalAnswer()
        {

        }

        public OriginalAnswer(string answer_text, Question question)
        {
            this.Answer_text = answer_text;
            this.Is_deleted = false;
            this.Question = question;
            this.Question_id = question.Id;
        }
    }
}