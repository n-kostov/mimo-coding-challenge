namespace LearningCenter.Domain.Exceptions
{
    public class InvalidUserAchievementException : BaseDomainException
    {
        public InvalidUserAchievementException()
        {
        }
        public InvalidUserAchievementException(string error) : base(error)
        {
        }
    }
}
