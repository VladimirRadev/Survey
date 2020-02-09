using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Models.View_Models {
    public class CreateQuestionViewModel {
        public String QuestionText { get; set; }
        public String RawAnswers { get; set; }
        public HashSet<Answer> answers { get; set; }
    }
}