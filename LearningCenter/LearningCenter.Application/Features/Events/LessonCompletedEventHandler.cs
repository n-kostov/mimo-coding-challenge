using LearningCenter.Application.Features.Services;
using MediatR;

namespace LearningCenter.Application.Features.Events
{
    public class LessonCompletedEventHandler : INotificationHandler<LessonCompletedEvent>
    {
        private readonly IUserAchievementService _achievementService;

        public LessonCompletedEventHandler(IUserAchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        public async Task Handle(LessonCompletedEvent notification, CancellationToken cancellationToken)
        {
            await _achievementService.TrackLessonCompletion(notification.UserId, notification.LessonId);
        }
    }
}
