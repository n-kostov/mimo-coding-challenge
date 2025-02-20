using LearningCenter.Application.Features;
using LearningCenter.Infrastructure.Persistence;
using LearningCenter.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearningCenter.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDbContext<LearningCenterDbContext>(options => options
                    .UseSqlite(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(LearningCenterDbContext).Assembly.FullName)))
                .AddScoped<IInitializer, LearningCenterDbInitializer>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IAchievementRepository, AchievementRepository>()
                .AddScoped<ICourseRepository, CourseRepository>()
                .AddScoped<IDatabaseSeeder, DatabaseSeeder>();
    }
}
