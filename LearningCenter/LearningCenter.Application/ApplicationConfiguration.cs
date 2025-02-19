using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LearningCenter.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}
