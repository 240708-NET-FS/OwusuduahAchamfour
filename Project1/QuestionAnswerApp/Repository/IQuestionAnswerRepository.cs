using QuestionAnswerConsoleApp.Entities;
using System.Collections.Generic;

namespace QuestionAnswerConsoleApp.Repository
{
    // Interface for the QuestionAnswerRepository
    public interface IQuestionAnswerRepository
    {
        void AddQuestion(Question question);
        List<Question> GetAllQuestions();
        Question? GetQuestionById(int id);
        void UpdateQuestion(Question question);
        void DeleteQuestion(int id);
        void AddAnswer(Answer answer);
    }
}
