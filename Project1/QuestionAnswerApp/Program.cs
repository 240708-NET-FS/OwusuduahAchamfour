using Microsoft.Extensions.DependencyInjection;
using QuestionAnswerConsoleApp.Controller;
using QuestionAnswerConsoleApp.Repository;
using QuestionAnswerConsoleApp.Service;
using System;

namespace QuestionAnswerConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
             
             // Set up Dependency Injection
             // Declare and instantiate serviceProvider for dependency injection
            var serviceProvider = new ServiceCollection()
                .AddScoped<IQuestionAnswerRepository, QuestionAnswerRepository>() // Register interface and implementation
                .AddScoped<QuestionAnswerService>() // Register QuestionAnswerService
                .AddScoped<QuestionAnswerController>() // Register QuestionAnswerController
                .BuildServiceProvider();

            // Declare controller and initialize with injection from QuestionAnswerController by serviceProvider
            var controller = serviceProvider.GetService<QuestionAnswerController>();

            //Check if something was indeed injected into controller
            if (controller != null)
            {
                // Run the application if something was injected into controller
                controller.Run();
            }
            else
            {
                Console.WriteLine("Ooops! Something went wrong."); 
                Console.WriteLine("Here is some tech jargon:"); 
                Console.WriteLine("The service provider did not inject the controller dependency. The controller is null.");
                Console.WriteLine("That basically means the service provider (think of it as a file) was not able to do");
                Console.WriteLine("what it is was supposed to with another file (the controller)");
                Console.WriteLine("Sorry...");
            }

        }
    }
}
