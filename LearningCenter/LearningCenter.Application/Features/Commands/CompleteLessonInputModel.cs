namespace LearningCenter.Application.Features.Commands
{
    public class CompleteLessonInputModel
    {
        internal CompleteLessonInputModel(int userId, int lessonId, DateTime startedOn, DateTime completedOn)
        {
            UserId = userId;
            LessonId = lessonId;
            StartedOn = startedOn;
            CompletedOn = completedOn;
        }

        public int UserId { get; }
        public int LessonId { get; }
        public DateTime StartedOn { get; }
        public DateTime CompletedOn { get; }
    }
}
