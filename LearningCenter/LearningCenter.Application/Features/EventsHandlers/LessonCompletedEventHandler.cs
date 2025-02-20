using LearningCenter.Application.Contracts;
using LearningCenter.Domain.Models.Users;
using MediatR;

namespace LearningCenter.Application.Features.EventHandlers
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
