using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Survey.Models
{
    ///<summary>
    /// A class for getting and inserting information in the database
    /// </summary>
    public static class DataProvider
    {
        ///<summary>
        /// Inserts a question in the database
        /// </summary>
        public static void InsertQuestion(Question question)
        {
            using (SurveyContext context = new SurveyContext())
            {
                context.Questions.Add(question);

                context.SaveChanges();
            }
        }

        ///<summary>
        /// Gets all non-deleted questions from the database
        /// </summary>
        public static List<Question> GetAllQuestions()
        {
            using (SurveyContext context = new SurveyContext())
            {
                return context.Questions.Where(a => a.Is_deleted == false).ToList();
            }
        }

        ///<summary>
        /// Gets all non-deleted questions, which are active at the moment, from the database
        /// </summary>
        public static List<Question> GetActiveQuestions()
        {
            using (SurveyContext context = new SurveyContext())
            {
                List<Question> allQuestions = GetAllQuestions();
                List<Question> activeQuestions = new List<Question>();
                foreach (var question in allQuestions)
                {
                    if (question.Start_date <= DateTime.Now.Date
                        && DateTime.Now.Date <= question.End_date)
                    {
                        activeQuestions.Add(question);
                    }
                }

                return activeQuestions;
            }
        }

        ///<summary>
        /// Gets a question by id from the database
        /// </summary>
        public static Question GetQuestion(int id)
        {
            using (SurveyContext context = new SurveyContext())
            {
                if (context.Questions.Where(a => a.Id == id).ToList().Count == 0)
                {
                    throw new IndexOutOfRangeException("Invalid id!");
                }

                return context.Questions.FirstOrDefault(a => a.Id == id);
            }
        }

        ///<summary>
        /// Edits a question by id from the database. Properties that can be editted: Question_text, Is_deleted, Start_date, End_date
        /// </summary>
        public static void EditQuestion(int id, Question editedQuestion)
        {
            using (SurveyContext context = new SurveyContext())
            {
                Question question = GetQuestion(id);

                question.Question_text = editedQuestion.Question_text;

                question.Is_deleted = editedQuestion.Is_deleted;

                question.Start_date = editedQuestion.Start_date;

                question.End_date = editedQuestion.End_date;

                context.SaveChanges();

            }
        }
        public static void DeleteQuestion(int id)
        {
            using (SurveyContext context = new SurveyContext())
            {
                context.Questions.FirstOrDefault(i => i.Id == id).Is_deleted = true;
                context.SaveChanges();

            }
        }

        ///<summary>
        /// Inserts an answer from a person in the database
        /// </summary>
        public static void InsertAnswerFromPerson(Answer answer)
        {
            using (SurveyContext context = new SurveyContext())
            {
                try
                {
                    //When answer.Question is set, EF automagically cretare record in DB for Question as well
                    answer.Question = null;

                    context.Answers.Add(answer);

                    context.SaveChanges();

                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }

        ///<summary>
        /// Inserts an origignal answer in the database
        /// </summary>
        public static void InsertOriginalAnswer(OriginalAnswer answer)
        {
            using (SurveyContext context = new SurveyContext())
            {
                //When answer.Question is set, EF automagically cretare record in DB for Question as well
                answer.Question = null;

                context.OriginalAnswers.Add(answer);

                context.SaveChanges();
            }
        }

        ///<summary>
        /// Gets all non-deleted answers for a question from the database
        /// </summary>
        public static List<Answer> GetAllAnswers(int questionId)
        {
            using (SurveyContext context = new SurveyContext())
            {
                return context.Answers
                    .Where(a => a.Is_deleted == false)
                    .Where(b => b.Question_id == questionId)
                    .ToList();
            }
        }

        ///<summary>
        /// Gets all non-deleted original answers for a question from the database
        /// </summary>
        public static List<OriginalAnswer> GetOriginalAnswers(int questionId)
        {
            using (SurveyContext context = new SurveyContext())
            {
                return context.OriginalAnswers
                    .Where(a => a.Is_deleted == false)
                    .Where(b => b.Question_id == questionId)
                    .ToList();
            }
        }

        ///<summary>
        /// Edits original answers for a question from the database
        /// </summary>
        public static void EditOriginalAnswers(int questionId, string newOriginalAnswer1, string newOriginalAnswer2)
        {
            using (SurveyContext context = new SurveyContext())
            {
                List<OriginalAnswer> originalAnswers = GetOriginalAnswers(questionId);

                if (originalAnswers.Count > 0)
                {
                    originalAnswers[0].Answer_text = newOriginalAnswer1;
                }

                if (originalAnswers.Count > 1)
                {
                    originalAnswers[1].Answer_text = newOriginalAnswer2;
                }

                context.SaveChanges();
            }
        }
    }
}