using LearningCenter.Application.Features.Commands;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LearningCenter.Web.SchemaFilters
{
    public class CompleteLessonCommandSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(CompleteLessonCommand))
            {
                schema.Example = new Microsoft.OpenApi.Any.OpenApiObject
                {
                    ["userId"] = new Microsoft.OpenApi.Any.OpenApiInteger(1),
                    ["lessonId"] = new Microsoft.OpenApi.Any.OpenApiInteger(2),
                    ["startedOn"] = new Microsoft.OpenApi.Any.OpenApiString("2025-02-16T12:26:00Z"),
                    ["completedOn"] = new Microsoft.OpenApi.Any.OpenApiString("2025-02-16T14:30:00Z")
                };
            }
        }
    }
}
