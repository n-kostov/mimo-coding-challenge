namespace LearningCenter.Domain.Exceptions
{
    public class InvalidAchievementException : BaseDomainException
    {
        public InvalidAchievementException()
        {
        }
        public InvalidAchievementException(string error) :base(error)
        {
        }
    }
}
