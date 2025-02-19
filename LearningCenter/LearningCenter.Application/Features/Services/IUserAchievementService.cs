namespace LearningCenter.Application.Features.Services
{
    public interface IUserAchievementService
    {
        Task TrackLessonCompletion(int userId, int lessonId);
    }
}
