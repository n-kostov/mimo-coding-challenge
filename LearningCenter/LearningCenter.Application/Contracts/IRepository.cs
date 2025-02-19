using LearningCenter.Domain.Common;

namespace LearningCenter.Application.Contracts
{
    public interface IRepository<out TEntity>
        where TEntity : IAggregateRoot
    {
    }
}
