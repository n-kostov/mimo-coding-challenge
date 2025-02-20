using LearningCenter.Application.Contracts;
using LearningCenter.Domain.Common;

namespace LearningCenter.Infrastructure.Persistence.Repositories
{
    internal abstract class DataRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IAggregateRoot
    {
        protected readonly LearningCenterDbContext db;

        protected DataRepository(LearningCenterDbContext db) => this.db = db;

        protected IQueryable<TEntity> All() => this.db.Set<TEntity>();

        protected Task<int> SaveChanges(CancellationToken cancellationToken = default)
            => this.db.SaveChangesAsync(cancellationToken);
    }
}
