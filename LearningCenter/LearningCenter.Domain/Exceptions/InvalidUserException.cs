namespace LearningCenter.Domain.Exceptions
{
    public class InvalidUserException : BaseDomainException
    {
        public InvalidUserException()
        {
        }
        public InvalidUserException(string error) : base(error)
        {
        }
    }
}
