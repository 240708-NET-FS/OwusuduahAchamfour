using QuestionAnswerConsoleApp.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace QuestionAnswerConsoleApp.Repository
{
    // Handles data operations for questions and answers
    public class QuestionAnswerRepository
    {
        // Adds a new question to the database
        public void AddQuestion(Question question)
        {
            using var context = new AppDbContext();
            context.Questions.Add(question);
            context.SaveChanges();
        }

        // Get all questions from the database
        public List<Question> GetAllQuestions()
        {
            using var context = new AppDbContext();
            return context.Questions.ToList();
        }

        // Get a question by ID
        public Question? GetQuestionById(int id)
        {
            using var context = new AppDbContext();
            if (context.Questions == null){
                Console.WriteLine("Nothing to display");
                }
            return context.Questions!
                          .Include(q => q.Answers)
                          .FirstOrDefault(q => q.Id == id);
            
        }

        // Updates an existing question
        public void UpdateQuestion(Question question)
        {
            using var context = new AppDbContext();
            context.Questions.Update(question);
            context.SaveChanges();
        }

        // Deletes a question by its ID
        public void DeleteQuestion(int id)
        {
            using var context = new AppDbContext();
            
            //Uses the Include() method as done in class           
            var question = context.Questions
                                  .Include(q => q.Answers)
                                  .FirstOrDefault(q => q.Id == id);
            if (question != null)
            {
                context.Questions.Remove(question);
                context.Answers.RemoveRange(question.Answers);
                context.SaveChanges();
            }
        }

        // Adds an answer to a question
        public void AddAnswer(Answer answer)
        {
            using var context = new AppDbContext();
            context.Answers.Add(answer);
            context.SaveChanges();
        }
    }
}
