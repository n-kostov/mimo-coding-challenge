using FluentValidation;
using FluentValidation.AspNetCore;
using LearningCenter.Application.Features.Commands;

namespace LearningCenter.Web
{
    public static class ValidationConfiguration
    {
        public static IServiceCollection AddValidation(
            this IServiceCollection services)
        {
            // Set global cascade mode to stop on first validation failure
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
            services.AddFluentValidationAutoValidation(options =>
            {
                options.DisableDataAnnotationsValidation = true;
            });
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<ComleteLessonCommandValidator>();

            return services;
        }
    }
}
