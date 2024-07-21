using System.Collections.Generic;

namespace QuestionAnswerConsoleApp.Entities
{
    // Represents a question in the system
    public class Question
    {
        public int Id { get; set; } // Unique identifier for the question
        public string? Text { get; set; } // The question text; string? makes question text nullable

        // Collection of answers related to this question
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();


        // Method to get a shortened version of the question text
        public string GetShortText()
        {
            if (Text == null){
                return string.Empty; //Text is null return empty string
            }
            
            if (Text.Length <= 50)
            {
                return Text;
            }
            
            return Text.Substring(0, 50) + "...";
        }
    }
}
