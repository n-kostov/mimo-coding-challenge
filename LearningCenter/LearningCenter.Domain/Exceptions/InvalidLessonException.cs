namespace LearningCenter.Domain.Exceptions
{
    public class InvalidLessonException : BaseDomainException
    {
        public InvalidLessonException()
        {
        }

        public InvalidLessonException(string error) => this.Error = error;
    }
}
