namespace LearningCenter.Domain.Exceptions
{
    public class InvalidLessonCompletedException : BaseDomainException
    {
        public InvalidLessonCompletedException()
        {
        }

        public InvalidLessonCompletedException(string error) => this.Error = error;
    }
}
