namespace LearningCenter.Application.Contracts
{
    public interface IUserAchievementService
    {
        Task TrackLessonCompletion(int userId, int lessonId);
    }
}
