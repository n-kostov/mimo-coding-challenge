using LearningCenter.Infrastructure;
using LearningCenter.Infrastructure.Persistence;

namespace LearningCenter.Web
{
    public static class ApplicationInitialization
    {
        public static IApplicationBuilder Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var initializers = serviceScope.ServiceProvider.GetServices<IInitializer>();

            foreach (var initializer in initializers)
            {
                initializer.Initialize();
            }

            var databaseSeeder = serviceScope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();
            databaseSeeder.Seed();

            return app;
        }
    }
}
