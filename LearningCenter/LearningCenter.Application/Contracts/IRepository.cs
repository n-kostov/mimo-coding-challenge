using LearningCenter.Domain.Common;

namespace LearningCenter.Application.Contracts
{
    public interface IRepository<out TEntity>
        where TEntity : IAggregateRoot
    {
        IQueryable<TEntity> All();

        Task<int> SaveChanges(CancellationToken cancellationToken = default);
    }
}
