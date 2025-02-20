using LearningCenter.Domain.Factories.Achievements;
using LearningCenter.Domain.Factories.Courses;
using LearningCenter.Domain.Factories.Users;
using LearningCenter.Domain.Models.Achievements;
using LearningCenter.Domain.Models.Courses;
using LearningCenter.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Numerics;

namespace LearningCenter.Tests.Common
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private SqliteConnection _connection;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove existing database context
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<LearningCenterDbContext>));
                if (descriptor != null) services.Remove(descriptor);

                // Create a new SQLite in-memory database connection
                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();

                // Register SQLite in-memory database
                services.AddDbContext<LearningCenterDbContext>(options =>
                    options.UseSqlite(_connection));

                // Ensure the database is seeded
                using var scope = services.BuildServiceProvider().CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<LearningCenterDbContext>();
                var userFactory = scopedServices.GetRequiredService<IUserFactory>();
                var courseFactory = scopedServices.GetRequiredService<ICourseFactory>();
                var achievementFactory = scopedServices.GetRequiredService<IAchievementFactory>();

                db.Database.Migrate();
                SeedTestData(db, userFactory, courseFactory, achievementFactory);
            });
        }

        private void SeedTestData(
            LearningCenterDbContext db, 
            IUserFactory userFactory, 
            ICourseFactory courseFactory, 
            IAchievementFactory achievementFactory)
        {
            var user = userFactory
                .WithName("Jon")
                .Build();
            db.Users.Add(user);

            var cCharpCourse = courseFactory
                .WithName("C#")
                .WithChapter("Chapter 1")
                    .WithLesson("Chapter 1", "Variables")
                    .WithLesson("Chapter 1", "Data Types")
                .Build();
            db.Courses.Add(cCharpCourse);

            var switftCourse = courseFactory
                .WithName("Swift")
                .WithChapter("Chapter 1")
                    .WithLesson("Chapter 1", "What is swift")
                    .WithLesson("Chapter 1", "Hello world")
                .WithChapter("Chapter 2")
                    .WithLesson("Chapter 2", "Variables")
                    .WithLesson("Chapter 2", "Functions")
                .Build();
            db.Courses.Add(switftCourse);
            db.SaveChanges();

            db.Achievements.Add(achievementFactory
                .WithName("Complete 5 lessons")
                .WithUnitType(AchievementUnitType.Lesson)
                .WithGoal(5)
                .Build());

            db.Achievements.Add(achievementFactory
                .WithName("Complete 25 lessons")
                .WithUnitType(AchievementUnitType.Lesson)
                .WithGoal(25)
                .Build());

            db.Achievements.Add(achievementFactory
                .WithName("Complete 50 lessons")
                .WithUnitType(AchievementUnitType.Lesson)
                .WithGoal(50)
                .Build());

            db.Achievements.Add(achievementFactory
                .WithName("Complete 1 chapter")
                .WithUnitType(AchievementUnitType.Chapter)
                .WithGoal(1)
                .Build());

            db.Achievements.Add(achievementFactory
                .WithName("Complete 5 chapters")
                .WithUnitType(AchievementUnitType.Chapter)
                .WithGoal(5)
                .Build());

            db.Achievements.Add(achievementFactory
                .WithName("Complete the Swift course")
                .WithUnitType(AchievementUnitType.Course)
                .WithTarget(switftCourse.Id)
                .Build());

            db.Achievements.Add(achievementFactory
                .WithName("Complete the C# course")
                .WithUnitType(AchievementUnitType.Course)
                .WithTarget(cCharpCourse.Id)
                .Build());

            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _connection?.Dispose();
        }
    }
}
