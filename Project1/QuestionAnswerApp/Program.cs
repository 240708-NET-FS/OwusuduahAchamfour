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
            // Declare and instantiate serviceProvider for dependency injection
            //AddSingleton<T>() method creates only one instance of the service
            var serviceProvider = new ServiceCollection()
                .AddSingleton<QuestionAnswerRepository>() 
                .AddSingleton<QuestionAnswerService>()
                .AddSingleton<QuestionAnswerController>()
                .BuildServiceProvider();

            // Declare controller and initialize with injection from serviceProvider
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
