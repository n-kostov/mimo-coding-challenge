using LearningCenter.Domain.Common;
using LearningCenter.Domain.Exceptions;

namespace LearningCenter.Domain.Models.Users
{
    public class LessonCompleted : ValueObject
    {
        public int LessonId { get; private set; }
        public DateTime StartedOn { get; private set; }
        public DateTime CompletedOn { get; private set; }

        private LessonCompleted() { }

        internal LessonCompleted(int lessonId, DateTime startedOn, DateTime completedOn)
        {
            Validate(startedOn, completedOn);

            LessonId = lessonId;
            StartedOn = startedOn;
            CompletedOn = completedOn;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return LessonId;
            yield return StartedOn;
            yield return CompletedOn;
        }

        private void Validate(DateTime startedOn, DateTime completedOn)
        {
            Guard.DateAfter<InvalidLessonException>(
                completedOn,
                startedOn,
                nameof(this.CompletedOn));
        }
    }
}
