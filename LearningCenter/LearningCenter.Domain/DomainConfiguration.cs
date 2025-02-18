using LearningCenter.Domain.Factories.Achievements;
using LearningCenter.Domain.Factories.Courses;
using LearningCenter.Domain.Factories.Users;
using Microsoft.Extensions.DependencyInjection;

namespace LearningCenter.Domain
{
    public static class DomainConfiguration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
            => services
                .AddTransient<ICourseFactory, CourseFactory>()
                .AddTransient<IUserFactory, UserFactory>()
                .AddTransient<IAchievementFactory, AchievementFactory>();
    }
}
