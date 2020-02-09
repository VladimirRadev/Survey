using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Models.View_Models {
    public class CreateQuestionViewModel {
        private String questionText { get; set; }
        private String rawAnswers { get; set; }
        private HashSet<Question> answers { get; set; }
    }
}