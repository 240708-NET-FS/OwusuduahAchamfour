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
        public QuestionAnswerService(QuestionAnswerRepository repo)
        {
            repository = repo;
        }

        public void AddQuestion(Question question)
        {
            if (string.IsNullOrWhiteSpace(question.Text) || question.Text.Length < 50)
            {
                throw new ArgumentException("Question text must be at least 50 characters.");
            }
            repository.AddQuestion(question);
        }

        public List<Question> GetAllQuestions()
        {
            return repository.GetAllQuestions();
        }

        public Question GetQuestionById(int id)
        {
            var question = repository.GetQuestionById(id);
            if (question == null)
            {
                throw new KeyNotFoundException("Question not found.");
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
