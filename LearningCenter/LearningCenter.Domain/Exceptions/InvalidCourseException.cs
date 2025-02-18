namespace LearningCenter.Domain.Exceptions
{
    public class InvalidCourseException : BaseDomainException
    {
        public InvalidCourseException()
        {
        }
        public InvalidCourseException(string error) : base(error)
        {
        }   
    }
}
