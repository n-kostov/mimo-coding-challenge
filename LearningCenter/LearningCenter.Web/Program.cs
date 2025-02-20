using LearningCenter.Application;
using LearningCenter.Domain;
using LearningCenter.Infrastructure;
using LearningCenter.Web;
using LearningCenter.Web.Middleware;
using LearningCenter.Web.SchemaFilters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDomain();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "LearningCenter API", Version = "v1" });
    options.SchemaFilter<CompleteLessonCommandSchemaFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseValidationExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Initialize();

app.Run();

public partial class Program { }