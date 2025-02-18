namespace LearningCenter.Domain.Exceptions
{
    public class InvalidLessonException : BaseDomainException
    {
        public InvalidLessonException()
        {
        }
        public InvalidLessonException(string error) : base(error)
        {
        }
    }
}
