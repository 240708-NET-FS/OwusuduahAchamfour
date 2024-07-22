using QuestionAnswerConsoleApp.Entities;
using QuestionAnswerConsoleApp.Repository;
using System.Collections.Generic;

namespace QuestionAnswerConsoleApp.Service
{
    // Handles business logic and interacts with the repository
    public class QuestionAnswerService
    {
        private readonly QuestionAnswerRepository repository;

        // Constructor for dependency injection
        public QuestionAnswerService(QuestionAnswerRepository questionAnswerRepository)
        {
            repository = questionAnswerRepository;
        }

        public void AddQuestion(Question question)
        {
            //Input validation: check to see if string is null, empty, or less than 50 characters
            if (string.IsNullOrWhiteSpace(question.Text) || question.Text.Length < 50)
            {
                throw new ArgumentException("I'm sorry, but I need at least 50 characters to work with.\nOperation aborted.");
            }
            repository.AddQuestion(question);
        }

        public List<Question> GetAllQuestions()
        {
            var questions = repository.GetAllQuestions();
            if (questions == null || !questions.Any())
            {
                throw new InvalidOperationException("Ouch! No questions were found in the questions table.");
            }
            return questions;
        }

        public Question GetQuestionById(int id)
        {
            var question = repository.GetQuestionById(id);
            if (question == null)
            {
                throw new KeyNotFoundException("Oh no! You entered a valid int, but no question has that ID.");
            }
            return question;
        }

        public void UpdateQuestion(Question question)
        {
            if (string.IsNullOrWhiteSpace(question.Text) || question.Text.Length < 50)
            {
                throw new ArgumentException("Question text must be at least 50 characters.");
            }
            repository.UpdateQuestion(question);
        }

        public void DeleteQuestion(int id)
        {
            //First check to see if question exists before deletion
            var question = repository.GetQuestionById(id);
            
            if (question == null)
             {
                throw new KeyNotFoundException("The question does not exist. Database not affected.");
            }

            repository.DeleteQuestion(id);
        }

        public void AddAnswer(Answer answer)
        {
            if (string.IsNullOrWhiteSpace(answer.Text) || answer.Text.Length < 50)
            {
                throw new ArgumentException("Answer text must be at least 50 characters.");
            }
            repository.AddAnswer(answer);
        }
    }
}
