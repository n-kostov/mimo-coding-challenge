using LearningCenter.Domain.Factories.Achievements;
using LearningCenter.Domain.Factories.Courses;
using LearningCenter.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
                var databaseSeeder = scopedServices.GetRequiredService<IDatabaseSeeder>();
                var courseFactory = scopedServices.GetRequiredService<ICourseFactory>();
                var achievementFactory = scopedServices.GetRequiredService<IAchievementFactory>();

                db.Database.Migrate();
                databaseSeeder.Seed();
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _connection?.Dispose();
        }
    }
}
