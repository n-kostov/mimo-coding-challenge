using LearningCenter.Application.Contracts;
using LearningCenter.Domain.Common;

namespace LearningCenter.Infrastructure.Persistence.Repositories
{
    internal class DataRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IAggregateRoot
    {
        private readonly LearningCenterDbContext db;

        public DataRepository(LearningCenterDbContext db) => this.db = db;

        public IQueryable<TEntity> All() => this.db.Set<TEntity>();

        public Task<int> SaveChanges(CancellationToken cancellationToken = default)
            => this.db.SaveChangesAsync(cancellationToken);
    }
}
