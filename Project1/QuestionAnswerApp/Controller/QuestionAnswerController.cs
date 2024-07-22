using Microsoft.EntityFrameworkCore.Storage.Internal;
using QuestionAnswerConsoleApp.Entities;
using QuestionAnswerConsoleApp.Service;
using System;

namespace QuestionAnswerConsoleApp.Controller
{
    // Handles user input and interaction
    public class QuestionAnswerController
    {
        private readonly QuestionAnswerService service;

        // Constructor for dependency injection
        public QuestionAnswerController(QuestionAnswerService questionAnswerService)
        {
            service = questionAnswerService;
        }

        public void Run()
        {
            while (true) //Creates an infitie loop
            {
                Console.Clear(); // Clears the console
                Console.WriteLine("=====================================");
                Console.WriteLine(" Welcome to the dotnet Q&A Console App ");
                Console.WriteLine("=====================================");
                Console.WriteLine("1. Create a Question");
                Console.WriteLine("2. View All Questions");
                Console.WriteLine("3. View Question By ID and Related Answers");
                Console.WriteLine("4. Update Question By ID");
                Console.WriteLine("5. Delete Question By ID");
                Console.WriteLine("6. Add Answer By Question ID");
                Console.WriteLine("7. Exit");
                Console.WriteLine("=====================================");
                Console.WriteLine("Please make the window bigger for a better user experience");
                Console.Write("Enter a number from the menu above: ");
                var choice = Console.ReadLine(); //Waits for user to input an option from the menu

                switch (choice)
                {
                    case "1":
                        CreateQuestion();
                        break; //Break prevents the subsequent case blocks from executing
                    case "2":
                        ViewAllQuestions();
                        break;
                    case "3":
                        ViewQuestionById();
                        break;
                    case "4":
                        UpdateQuestionById();
                        break;
                    case "5":
                        DeleteQuestionById();
                        break;
                    case "6":
                        AddAnswerByQuestionId();
                        break;
                    case "7":
                        return; //This terminates the infite loop created by the while(true) and ends application
                    default:
                        Console.WriteLine("Oops. You can only pick from the menu options.");
                        break;
                }
                    //Here is where execution will come after any break is reached in the switch case above
                    // Prompt the user to press any key to continue
                    Console.WriteLine("Press any key to continue...I'll just wait.");
                    Console.ReadKey(); // Waits for the user to press a key
            }
        }

        private void CreateQuestion()
        {
            Console.WriteLine("Think of a creative dotnet question. Ask it here (at least 50 characters including spaces):");
            var text = Console.ReadLine();
           

            var question = new Question { Text = text };

            try
            {
                service.AddQuestion(question);
                Console.WriteLine("Awesome. Question was added to the database.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);  
            }
        }

        private void ViewAllQuestions()
        {
            try{
                var questions = service.GetAllQuestions();
                foreach (var question in questions)
                {
                    //Only show the first fifty characters of the question text by calling GetShortText()
                    Console.WriteLine($"Question ID: {question.Id} -> {question.GetShortText()}");
                }

            }
            catch(InvalidOperationException ex){
                Console.WriteLine(ex.Message);
            }
            
        }

        private void ViewQuestionById()
        {
            Console.WriteLine("Okay. So I see you want to view a question.");
            Console.WriteLine("This will also show you the responses to the question.");
            Console.WriteLine("Enter the ID of the question you want:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    var question = service.GetQuestionById(id);
                    Console.WriteLine($"Question ID: {question.Id}, Text: {question.Text}");
                
                    foreach (var answer in question.Answers)
                    {
                        Console.WriteLine($"  Answer ID: {answer.Id}, Text: {answer.Text}");
                    }
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Sorry...No question has that ID. So Invalid ID.");
            }
        }

        private void UpdateQuestionById()
        {
            Console.WriteLine("Enter the ID of the question you want to update:");
            
            //Controller checks to see if id is a valid int, if so,
            //id is passed to the service for further checking to see if
            //id is the id of a question in the db
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try{
                var question = service.GetQuestionById(id);
                Console.WriteLine($"Here is the current text:");
                Console.WriteLine($"{question.Text}");
                Console.WriteLine();
                Console.WriteLine("Enter the text for your new question");
                Console.WriteLine("At least 50 characters including spaces");
                var newText = Console.ReadLine();

                question.Text = newText;

                try
                {
                    service.UpdateQuestion(question);
                    Console.WriteLine("Question updated successfully.");
                }
                catch (ArgumentException ex) //Handle the case where newText < 50 characters
                {
                    Console.WriteLine(ex.Message);
                }

                }catch(KeyNotFoundException ex){ //Handle the case where GetQuestionById() does not find Id
                    Console.WriteLine(ex.Message);
                }
        
            }else{
                //Handle the case where user input for Id is not a valid int in the first place
                Console.WriteLine("You did not enter an int");
            }
        }


        private void DeleteQuestionById()
        {
            Console.WriteLine("Please provide me the ID of the question to delete:");
            //Some input validation in the controller: if user input parses to an int, output a variable called
            //id as declared in the argument of TryParse(), and initialize id with the int from user input
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try{
                service.DeleteQuestion(id);
                Console.WriteLine("Hope you wanted that question deleted. Because the question deleted successfully.");
                }catch(KeyNotFoundException ex){
                    Console.WriteLine(ex.Message);
                }
            }
            else{
                //Handle the situation where user does not input an int
                Console.WriteLine("You did not provide a valid integer...Operation aborted.");
            }
         
           }

        private void AddAnswerByQuestionId()
        {
            Console.WriteLine("Enter question ID to add an answer to:");
            if (int.TryParse(Console.ReadLine(), out int questionId))
            {
            
            //First check to see if the Question ID exists in the database
            try{
                var question = service.GetQuestionById(questionId);
            }
            catch(KeyNotFoundException ex){
                //Handle the case where input is a valid int, but not a valid questionId
                Console.WriteLine(ex.Message);
                return;
            }
           
            
            Console.WriteLine("Think of a great response: (I need at least 50 characters including spaces):");
            var text = Console.ReadLine();
                            
                var answer = new Answer { Text = text, QuestionId = questionId };

                try
                {
                    service.AddAnswer(answer);
                    Console.WriteLine("Thanks for the response. Answer added successfully to the database.");
                }
                catch (ArgumentException ex)
                {//Handle the case where input validation fails for Answer.text
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {//Handle the case where input is not a valid int in the first place
                Console.WriteLine("Sorry, but that ID was definitely not a valid int.");
            }
        }
    }
}
