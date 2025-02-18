using LearningCenter.Domain.Models.Achievements;
using LearningCenter.Domain.Models.Courses;
using LearningCenter.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LearningCenter.Infrastructure.Persistence
{
    internal class LearningCenterDbContext : DbContext
    {
        public LearningCenterDbContext(DbContextOptions<LearningCenterDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Achievement> Achievements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
