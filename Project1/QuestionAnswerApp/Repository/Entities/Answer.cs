namespace QuestionAnswerConsoleApp.Entities
{
    // Represents an answer to a question
    public class Answer
    {
        public int Id { get; set; } // Unique identifier for the answer
        public string? Text { get; set; } // The answer text; nullable string
        public int QuestionId { get; set; } // Foreign key to the related question

        // Navigation property to the related question
        public Question? Question { get; set; } //nullable Question due to question mark after Question
    }
}
