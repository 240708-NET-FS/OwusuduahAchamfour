using Microsoft.EntityFrameworkCore;
using QuestionAnswerConsoleApp.Entities;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace QuestionAnswerConsoleApp.Repository
{
    // Represents the database context for the application
    public class AppDbContext : DbContext
    {

        public DbSet<Question> Questions { get; set; } // Table for questions
        public DbSet<Answer> Answers { get; set; } // Table for answers

    
        
        // Configures the database connection string using optionsBuilder
        //As we did in class
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                                    .SetBasePath(Directory.GetCurrentDirectory())
                                                    .AddJsonFile("appsettings.json")
                                                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        // Configures the relationships between entities
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
     
            //Explicitly configure one relationship and let EF do the rest
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId);
        }
    }
}
