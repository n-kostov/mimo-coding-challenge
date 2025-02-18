using LearningCenter.Domain.Common;

namespace LearningCenter.Domain.Factories
{
    public interface IFactory<out TEntity>
        where TEntity : IAggregateRoot
    {
        TEntity Build();
    }
}
