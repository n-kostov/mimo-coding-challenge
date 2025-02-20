using LearningCenter.Domain.Common;
using LearningCenter.Domain.Models.Achievements;
using LearningCenter.Domain.Models.Courses;
using LearningCenter.Domain.Models.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LearningCenter.Infrastructure.Persistence
{
    public class LearningCenterDbContext : DbContext
    {
        private readonly IMediator _dispatcher;
        public LearningCenterDbContext(DbContextOptions<LearningCenterDbContext> options, IMediator mediator)
            : base(options)
        {
            _dispatcher = mediator;
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

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEventEntities = ChangeTracker.Entries<Entity<int>>()
                .Select(po => po.Entity)
                .Where(po => po.DomainEvents.Any())
                .ToArray();

            foreach (var entity in domainEventEntities)
            {
                var events = entity.DomainEvents.ToArray();
                entity.ClearDomainEvents();
                foreach (var domainEvent in events)
                {
                    _dispatcher.Publish(domainEvent);
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}