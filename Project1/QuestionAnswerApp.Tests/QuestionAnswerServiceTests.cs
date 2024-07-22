using QuestionAnswerConsoleApp.Entities;
using QuestionAnswerConsoleApp.Service;
using QuestionAnswerConsoleApp.Repository;
using Xunit;
using Moq;

namespace QuestionAnswerConsoleApp.Tests
{
    public class QuestionAnswerServiceTests
    {
      
        [Fact]
        public void AddQuestion_ShouldCallRepositoryAddQuestion_WhenQuestionIsValid()
        {
            // Arrange
            var validQuestion = new Question
            {
                Text = "This is a valid question with more than 50 characters for testing purposes."
            };

            // Create a mock of IQuestionAnswerRepository
            //Provide Mock with the interface and Mock mocks the implementation
            var mockRepository = new Mock<IQuestionAnswerRepository>();

            //Create an instance of QuestionAnswerService with the mock repository
            //instead of the acutal repository. Because the actual repository will try to
            //talk to the remote db, which is not what we want for testing
            var service = new QuestionAnswerService(mockRepository.Object);

            // Act
            service.AddQuestion(validQuestion);

            // Assert
            // Verify that AddQuestion on the repository was called exactly once with the valid question
            mockRepository.Verify(repo => repo.AddQuestion(It.Is<Question>(q => q.Text == validQuestion.Text)), Times.Once);
        }
    }
}
