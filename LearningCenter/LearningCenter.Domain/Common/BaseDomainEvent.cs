namespace LearningCenter.Domain.Common
{
    public abstract class BaseDomainEvent
    {
        public DateTime OccurredOn { get; private set; } = DateTime.UtcNow;
    }
}
