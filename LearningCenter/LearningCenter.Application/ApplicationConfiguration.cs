using LearningCenter.Application.Contracts;
using LearningCenter.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LearningCenter.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services
                .AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddTransient<IUserAchievementService, UserAchievementService>();
    }
}
