using LearningCenter.Domain.Common;
using MediatR;

namespace LearningCenter.Application.Features.Events
{
    public class LessonCompletedEvent : BaseDomainEvent, INotification
    {
        public int UserId { get; }
        public int LessonId { get; }
        public DateTime CompletedOn { get; }

        public LessonCompletedEvent(int userId, int lessonId, DateTime completedOn)
        {
            UserId = userId;
            LessonId = lessonId;
            CompletedOn = completedOn;
        }
    }
}
