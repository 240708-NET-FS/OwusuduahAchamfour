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
                //If question is not valid, the service method will throw an exception upon input validation.
                //So the repository method will not even get called.
                //So if repository method is called, then question is valid.
                // Verify that AddQuestion on the repository was called exactly once with the valid question
            mockRepository.Verify(repo => repo.AddQuestion(It.Is<Question>(q => q.Text == validQuestion.Text)), Times.Once);
        }


        [Fact]
        public void AddQuestion_ShouldNotThrowException_WhenQuestionIsValid()
        {
            // Arrange
            var validQuestion = new Question
            {
                Text = "This is a valid question with more than 50 characters for testing purposes."
            };

            var mockRepository = new Mock<IQuestionAnswerRepository>(); // Create a mock of IQuestionAnswerRepository

            var service = new QuestionAnswerService(mockRepository.Object); // Create an instance of QuestionAnswerService with the mock repository


            // Act
            var exception = Record.Exception(() => service.AddQuestion(validQuestion));
            
            //Assert
            Assert.Null(exception); //Assert that no exception will be thrown if a valid question

            // Verify that AddQuestion on the repository was called exactly once with the valid question
            mockRepository.Verify(repo => repo.AddQuestion(It.Is<Question>(q => q.Text == validQuestion.Text)), Times.Once);
        }

    }
}
